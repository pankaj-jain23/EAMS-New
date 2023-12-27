using EAMS_ACore.HelperModels;
using EAMS_ACore.NotificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IRepository
{
    public interface INotificationRepository
    {
        Task<ServiceResponse> SendNotification(Notification notificationModel);
        Task<List<Notification>> GetNotification();
    }
}
