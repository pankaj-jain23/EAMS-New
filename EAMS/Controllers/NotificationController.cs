using AutoMapper;
using EAMS.ViewModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.NotificationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [Route("SendNotification")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationViewModel notificationViewModel)
        {
            var mappedData=_mapper.Map<Notification>(notificationViewModel);
            var result = await _notificationService.SendNotification(mappedData);
            
            return Ok(result);
        }
        [Route("GetNotification")]
        [HttpGet]
        public async Task<IActionResult> GetNotification()
        { 
            var result = await _notificationService.GetNotification();

            return Ok(result);
        }
    }
}
