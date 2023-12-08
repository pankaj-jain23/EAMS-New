using EAMS_ACore.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EAMS.Hubs
{
    public sealed class DashboardHub : Hub
    {
        private readonly IEamsService _eamsService;

        public DashboardHub(IEamsService eamsService)
        {
            _eamsService = eamsService;
        }

        public override async Task OnConnectedAsync()
        {
            // Call the method to get the dashboard count
            var dashboardCount = await _eamsService.SendDashBoardCount();

            // Send the dashboard count to all connected clients
            await Clients.All.SendAsync("ReceivedDashBoardCount", dashboardCount);
        }

        public async Task SendDashBoardCount()
        {
            // Call the method to get the dashboard count
            var dashboardCount = await _eamsService.SendDashBoardCount();

            // Send the dashboard count to all connected clients
            await Clients.All.SendAsync("ReceivedDashBoardCount", dashboardCount);
        }
    }
}
