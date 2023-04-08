using PicPig.Enums;
using PicPig.Models;

namespace PicPig.Extensions;

public static class ConfigurationExtensions
{
    public static string? GetTelegramBotApiKey(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(OptionSections.PicPig)).GetValue<string>(nameof(PicPigOptions.TelegramBotApiKey));
    }

    public static string? GetStableDiffusionApiAddress(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(OptionSections.PicPig)).GetValue<string>(nameof(PicPigOptions.StableDiffusionApiAddress));
    }
}
