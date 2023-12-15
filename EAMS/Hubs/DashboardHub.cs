using EAMS_ACore.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EAMS.Hubs
{
    //public class DashBoardHub : Hub
    //{
    //    private readonly IEamsService _eamsService;
    //    public DashBoardHub(IEamsService eamsService)
    //    {
    //        _eamsService = eamsService;
    //    }
    //    public async Task GetAndBroadcastDashboardCount()
    //    {
    //        var latestRecord = await _eamsService.GetDashBoardCount();
    //        await Clients.All.SendAsync("GetDashboardCount", latestRecord);
    //    }
    //    public override async Task OnConnectedAsync()
    //    {
    //        await base.OnConnectedAsync();
    //    }

    //    public override async Task OnDisconnectedAsync(Exception exception)
    //    {
    //        await base.OnDisconnectedAsync(exception);
    //    }

    //}
}
