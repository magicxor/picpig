using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Extensions;
using PicPig.Models;
using PicPig.Services;
using Polly;
using Polly.Extensions.Http;
using StableDiffusionClient;
using Telegram.Bot;

namespace PicPig;

public class Program
{
    private static readonly IAsyncPolicy<HttpResponseMessage> HttpRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 1.5));

    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .Configure<PicPigOptions>(hostContext.Configuration.GetSection(nameof(OptionSections.PicPig)));

                services.AddHttpClient(nameof(HttpClientTypes.WaitAndRetryOnTransientHttpError))
                    .AddPolicyHandler(HttpRetryPolicy);

                var stableDiffusionApiAddress = hostContext.Configuration.GetStableDiffusionApiAddress()
                                                ?? throw new ServiceException(LocalizationKeys.Errors.Configuration.StableDiffusionApiAddressMissing);
                services.AddScoped<Client>(s => new Client(stableDiffusionApiAddress,
                    s.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(nameof(HttpClientTypes.WaitAndRetryOnTransientHttpError))));

                var telegramBotApiKey = hostContext.Configuration.GetTelegramBotApiKey()
                                        ?? throw new ServiceException(LocalizationKeys.Errors.Configuration.TelegramBotApiKeyMissing);
                services.AddScoped<ITelegramBotClient, TelegramBotClient>(s => new TelegramBotClient(telegramBotApiKey,
                    s.GetRequiredService<IHttpClientFactory>()
                        .CreateClient(nameof(HttpClientTypes.WaitAndRetryOnTransientHttpError))));

                services.AddScoped<PresetFactoryProvider>();
                services.AddScoped<Txt2ImgQueryParser>();
                services.AddScoped<Txt2ImgService>();
                services.AddScoped<TelegramBotService>();

                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }
}
