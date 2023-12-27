using EAMS_ACore.HelperModels;
using EAMS_ACore.NotificationModels;

namespace EAMS_ACore.Interfaces
{
    public interface INotificationService
    {
        Task<ServiceResponse> SendNotification(Notification notificationModel);
        Task<List<Notification>> GetNotification();

    }
}
