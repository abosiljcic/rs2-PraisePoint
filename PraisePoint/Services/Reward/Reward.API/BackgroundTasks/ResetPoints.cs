namespace Reward.API.BackgroundTasks;
using Microsoft.Extensions.Hosting;
using Reward.API.Repositories.Interfaces;
using System.Threading.Tasks;

public class ResetPoints : IHostedService, IDisposable
{
    private readonly ILogger<ResetPoints> _logger;
    private readonly IPointsRepository _pointsRepository;
    private Timer? _timer;

    private const int DefaultBudget = 1000;

    public ResetPoints(ILogger<ResetPoints> logger, IPointsRepository pointsRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _pointsRepository = pointsRepository ?? throw new ArgumentNullException(nameof(pointsRepository));
    }

    //oslobadja resurse vezane za tajmer
    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Zakazivanje zadatka da se pokrene odmah i ponavlja svakih 30 dana
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(30));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        if (state is not CancellationToken cancellationToken)
        {
            _logger.LogWarning("State is not a CancellationToken.");
            return;
        }

        _logger.LogInformation("Resetting points...");

        try
        {
            await ResetUserPointsAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while resetting points.");
        }
    }

    private async Task ResetUserPointsAsync(CancellationToken cancellationToken)
    {
        var points = await _pointsRepository.GetAllPoints();
        foreach (var point in points)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogWarning("Operation canceled.");
                return;
            }
            point.received_points = 0;
            point.budget = DefaultBudget;
            await _pointsRepository.UpdateUserAsync(point);
        }

        _logger.LogInformation("Points have been reset for all users.");
    }
}

