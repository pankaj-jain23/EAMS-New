using EAMS.Hubs;
using EAMS_ACore.Interfaces;
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

        var notification = conn.WaitAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await notification;

            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IEamsService>();
                var currentRecord = await scopedService.GetDashBoardCount();
                var latestRecord = await scopedService.GetDashBoardCount(); // Fetch the latest record

                // Check if there is an actual change in the database
                if (!currentRecord.Equals(latestRecord))
                {
                    // Call the method to send the update to clients
                    await _hubContext.Clients.All.SendAsync("GetDashboardCount", latestRecord);
                }
            }

            // Start waiting for the next notification
            notification = conn.WaitAsync(stoppingToken);
        }
    }
}
