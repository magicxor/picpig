using System.ComponentModel.DataAnnotations;

namespace PicPig.Models;

public class PicPigOptions
{
    [Required]
    [RegularExpression(@".*:.*")]
    public required string TelegramBotApiKey { get; init; }

    [Required]
    public required long BotOwnerUserId { get; init; }

    [Required]
    public required long MediaCacheGroupChatId { get; init; }

    [Required]
    public required string LoadingProgressJpegBase64 { get; init; }

    [Required]
    public required string LoadingProgressImageId { get; init; }

    [Required]
    [Url]
    public required string StableDiffusionApiAddress { get; set; }
}
