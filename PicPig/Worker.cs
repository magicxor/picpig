using PicPig.Services;

namespace PicPig;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private TelegramBotService? _telegramBotService;

    public Worker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();

        _telegramBotService = serviceScope.ServiceProvider.GetRequiredService<TelegramBotService>();
        _telegramBotService.Start(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }
}
