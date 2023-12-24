using EAMS_ACore.NotificationModels;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using EAMS_ACore.Interfaces;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
namespace EAMS_BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository   _notificationRepository;
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings ,INotificationRepository notificationRepository)
        {
            _fcmNotificationSetting = settings.Value;
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetNotification()
        {
           return await _notificationRepository.GetNotification();  
        }

        public async Task<ServiceResponse> SendNotification(Notification notificationModel)
        {
            return await _notificationRepository.SendNotification(notificationModel);
        }
    }
}
