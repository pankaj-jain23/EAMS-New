using EAMS.Hubs;
using EAMS_ACore.Interfaces;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
public class DatabaseListenerService : BackgroundService
{
    private readonly IHubContext<DashBoardHub> _hubContext;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _connectionString = "Host=10.44.86.109;Port=5432;Database=EAMS;Username=postgres;Password=postgres;Timeout=380;CommandTimeout=7200;";

    public DatabaseListenerService(IHubContext<DashBoardHub> hubContext, IServiceScopeFactory scopeFactory)
    {
        _hubContext = hubContext;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand("LISTEN dashboard_change", conn);
        await cmd.ExecuteNonQueryAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            // Wait for a notification or cancellation token
            var notification = conn.WaitAsync(stoppingToken);

            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IEamsService>();

                // Fetch the latest record
                var latestRecord = await scopedService.GetDashBoardCount();

                // Notify clients if there is a change
                await NotifyClientsIfChanged(latestRecord);
            }

            // Start waiting for the next notification
            await notification;
        }
    }

    private async Task NotifyClientsIfChanged(DashBoardRealTimeCount latestRecord)
    {
        try
        {
            // You may want to handle exceptions and errors here
            await _hubContext.Clients.All.SendAsync("GetDashboardCount", latestRecord);
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Error notifying clients: {ex.Message}");
        }
    }
}


