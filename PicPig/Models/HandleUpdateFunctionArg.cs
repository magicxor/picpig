using Telegram.Bot;
using Telegram.Bot.Types;

namespace PicPig.Models;

public record HandleUpdateFunctionArg(
    ITelegramBotClient BotClient,
    Update Update,
    CancellationToken CancellationToken
);
