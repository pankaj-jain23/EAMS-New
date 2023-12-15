using EAMS.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Npgsql;

public class DatabaseListenerService : BackgroundService
{
    private readonly IHubContext<DashBoardHub> _hubContext;
    private readonly string _connectionString = "Host=10.44.86.109;Port=5432;Database=EAMS;Username=postgres;Password=postgres;Timeout=380;CommandTimeout=7200;";

    public DatabaseListenerService(IHubContext<DashBoardHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand("LISTEN dashboard_change", conn);
        await cmd.ExecuteNonQueryAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            await conn.WaitAsync(stoppingToken);
            await _hubContext.Clients.All.SendAsync("GetAndBroadcastDashboardCount");
        }
    }
}
