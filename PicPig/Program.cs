using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Extensions;
using PicPig.Models;
using PicPig.Services;
using Polly;
using Polly.Extensions.Http;
using StableDiffusionClient;
using Telegram.Bot;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PicPig;

public class Program
{
    private static readonly IAsyncPolicy<HttpResponseMessage> HttpRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 1.5));

    private static readonly LoggingConfiguration LoggingConfiguration = new XmlLoggingConfiguration("nlog.config");

    public static void Main(string[] args)
    {
        // NLog: setup the logger first to catch all errors
        LogManager.Configuration = LoggingConfiguration;
        try
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(LoggingConfiguration);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddOptions<PicPigOptions>()
                    .Bind(hostContext.Configuration.GetSection(nameof(OptionSections.PicPig)))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

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
        catch (Exception ex)
        {
            // NLog: catch setup errors
            LogManager.GetCurrentClassLogger(typeof(Program)).Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }
}
