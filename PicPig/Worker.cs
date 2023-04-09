using System.Diagnostics;
using Microsoft.Extensions.Options;
using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Models;
using PicPig.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace PicPig;

public class Worker : BackgroundService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly PicPigOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private static readonly ReceiverOptions ReceiverOptions = new()
    {
        // receive all update types
        AllowedUpdates = Array.Empty<UpdateType>(),
    };

    private string? _loadingProgressPictureId;
    private Txt2ImgService? _txt2ImgService;

    public Worker(IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger,
        IOptions<PicPigOptions> options,
        IServiceScopeFactory serviceScopeFactory)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _options = options.Value;
        _serviceScopeFactory = serviceScopeFactory;
    }

    private static string Cut(string src, int maxLength, string? defaultStr = null)
    {
        if (string.IsNullOrEmpty(src) && !string.IsNullOrEmpty(defaultStr))
            return defaultStr;
        return src.Length <= maxLength ? src : src[..(maxLength - 1)];
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogTrace("Received update with type={update}", update.Type.ToString());

            if (_loadingProgressPictureId == null)
            {
                throw new ServiceException(LocalizationKeys.Errors.Telegram.LoadingProgressPictureIdIsNull);
            }

            if (update.InlineQuery is not null)
            {
                var inlineQuery = update.InlineQuery;

                await botClient.AnswerInlineQueryAsync(inlineQuery.Id, new List<InlineQueryResult>
                {
                    new InlineQueryResultCachedPhoto(Guid.NewGuid().ToString(), _loadingProgressPictureId)
                    {
                        Caption = Cut(update.InlineQuery.Query, 256, "🎲"),
                        ReplyMarkup = InlineKeyboardButton.WithCallbackData("Please wait..."),
                    },
                }, cancellationToken: cancellationToken);
            }
            else if (update.ChosenInlineResult is { InlineMessageId: not null } chosenInlineResult)
            {
                _logger.LogInformation("Query: {query}", update.ChosenInlineResult?.Query);

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
                                caption: $"{txt2ImgResult.Query.PresetFactory.GetType().Name}: {(txt2ImgResult.Query.IgnoreDefaultPrompt ? "!" : "")}{txt2ImgResult.Query.UserPositivePrompt}",
                                cancellationToken: cancellationToken);
                        }
                    },
                    exception =>
                    {
                        // todo: rewrite this file
                    }
                );
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while handling update");
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
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

    private async Task<string> UploadLoadingProgressPictureAsync(ITelegramBotClient telegramBotClient, CancellationToken cancellationToken)
    {
        var jpegBytes = Convert.FromBase64String(_options.LoadingProgressJpegBase64);
        using var memoryStream = new MemoryStream(jpegBytes);
        var message = await telegramBotClient.SendPhotoAsync(_options.MediaCacheGroupChatId,
            new InputMedia(memoryStream, "LoadingProgress"),
            cancellationToken: cancellationToken);
        return message.Photo?.FirstOrDefault()?.FileId ?? throw new ServiceException(LocalizationKeys.Errors.Telegram.ErrorObtainingPhotoId);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();

        _txt2ImgService = serviceScope.ServiceProvider.GetRequiredService<Txt2ImgService>();
        var telegramBotClient = serviceScope.ServiceProvider.GetRequiredService<TelegramBotClient>();

        telegramBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: ReceiverOptions,
            cancellationToken: stoppingToken
        );

        _loadingProgressPictureId = await UploadLoadingProgressPictureAsync(telegramBotClient, stoppingToken);
        _logger.LogInformation("Loading progress picture uploaded: {_loadingProgressPictureId}", _loadingProgressPictureId);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
