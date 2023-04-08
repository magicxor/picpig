using PicPig.Enums;
using PicPig.Exceptions;
using PicPig.Extensions;
using PicPig.Models;
using PicPig.Services;
using StableDiffusionClient;
using Telegram.Bot;

namespace PicPig;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .Configure<PicPigOptions>(hostContext.Configuration.GetSection(nameof(OptionSections.PicPig)));

                var stableDiffusionApiAddress = hostContext.Configuration.GetStableDiffusionApiAddress()
                                                ?? throw new ServiceException(LocalizationKeys.Errors.Configuration.StableDiffusionApiAddressMissing);
                services.AddScoped<Client>(_ => new Client(stableDiffusionApiAddress, new HttpClient()));

                var telegramBotApiKey = hostContext.Configuration.GetTelegramBotApiKey()
                                        ?? throw new ServiceException(LocalizationKeys.Errors.Configuration.TelegramBotApiKeyMissing);
                services.AddScoped<TelegramBotClient>(_ => new TelegramBotClient(telegramBotApiKey));

                services.AddScoped<PresetFactoryProvider>();
                services.AddScoped<Txt2ImgQueryParser>();
                services.AddScoped<Txt2ImgService>();

                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }
}
