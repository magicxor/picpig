using Humanizer;
using Microsoft.Extensions.Options;
using PicPig.Extensions;
using PicPig.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace PicPig.Services;

public class TelegramBotService
{
    private readonly ILogger<TelegramBotService> _logger;
    private readonly PicPigOptions _options;
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly Txt2ImgService _txt2ImgService;

    private static readonly ReceiverOptions ReceiverOptions = new()
    {
        // receive all update types
        AllowedUpdates = Array.Empty<UpdateType>(),
    };

    public TelegramBotService(ILogger<TelegramBotService> logger,
        IOptions<PicPigOptions> options,
        ITelegramBotClient telegramBotClient,
        Txt2ImgService txt2ImgService)
    {
        _logger = logger;
        _options = options.Value;
        _telegramBotClient = telegramBotClient;
        _txt2ImgService = txt2ImgService;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("Received update with type={update}", update.Type.ToString());
        ThreadPool.QueueUserWorkItem(async _ => await HandleUpdateFunction(botClient, update, cancellationToken));
    }

    private async Task HandleUpdateFunction(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            if (update.InlineQuery is { } inlineQuery)
            {
                _logger.LogInformation("Inline query received. Query (length: {queryLength}): {query}",
                    inlineQuery.Query.Length,
                    inlineQuery.Query);

                await botClient.AnswerInlineQueryAsync(inlineQuery.Id, new List<InlineQueryResult>
                {
                    new InlineQueryResultCachedPhoto(Guid.NewGuid().ToString(), _options.LoadingProgressImageId)
                    {
                        Caption = inlineQuery.Query.Cut(256, "🎲"),
                        ReplyMarkup = InlineKeyboardButton.WithCallbackData("Please wait..."),
                    },
                }, cancellationToken: cancellationToken);
            }
            else if (update.ChosenInlineResult is { InlineMessageId: not null } chosenInlineResult)
            {
                _logger.LogInformation("Inline result chosen. Query (length: {queryLength}): {query}",
                    chosenInlineResult.Query.Length,
                    chosenInlineResult.Query);

                var generateImageResult = await _txt2ImgService.GenerateImageAsync(chosenInlineResult.Query, cancellationToken);
                generateImageResult.Switch(
                    async txt2ImgResult =>
                    {
                        var photoMsg = await botClient.SendPhotoAsync(_options.MediaCacheGroupChatId,
                            new InputMedia(txt2ImgResult.ImageStream, "StableDiffusionImage"), cancellationToken: cancellationToken);
                        if (photoMsg.Photo != null)
                        {
                            await botClient.EditMessageMediaAsync(
                                inlineMessageId: chosenInlineResult.InlineMessageId,
                                media: new InputMediaPhoto(new InputMedia(photoMsg.Photo.First().FileId)),
                                cancellationToken: cancellationToken);
                            await botClient.EditMessageCaptionAsync(
                                inlineMessageId: chosenInlineResult.InlineMessageId,
                                caption: $"{txt2ImgResult.Query.PresetFactory.GetType().Name} ({txt2ImgResult.ElapsedTime.Humanize()}): {(txt2ImgResult.Query.IgnoreDefaultPrompt ? "!" : "")}{txt2ImgResult.Query.UserPositivePrompt}",
                                cancellationToken: cancellationToken);
                        }
                    },
                    async exception =>
                    {
                        _logger.LogError(exception, "Error while generating image");
                        await botClient.EditMessageCaptionAsync(
                            inlineMessageId: chosenInlineResult.InlineMessageId,
                            caption: $"ERROR processing {update.ChosenInlineResult?.Query}: {exception}",
                            cancellationToken: cancellationToken);
                    }
                );
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while handling update");
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ApiRequestException apiRequestException)
        {
            _logger.LogError(exception,
                @"Telegram API Error. ErrorCode={ErrorCode}, RetryAfter={RetryAfter}, MigrateToChatId={MigrateToChatId}",
                apiRequestException.ErrorCode,
                apiRequestException.Parameters?.RetryAfter,
                apiRequestException.Parameters?.MigrateToChatId);
        }
        else
        {
            _logger.LogError(exception, @"Telegram API Error");
        }

        return Task.CompletedTask;
    }

    public void Start(CancellationToken cancellationToken)
    {
        _telegramBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: ReceiverOptions,
            cancellationToken: cancellationToken
        );
    }
}
