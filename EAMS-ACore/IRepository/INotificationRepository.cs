using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
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
        Task<ServiceResponse> AddSMSTemplate(SMSTemplate SMSModel);
        Task<SMSTemplate> GetSMSTemplateById(string smsTemplateMasterId);
        Task<ServiceResponse> SaveSMS(SMSSentModel sMSSentModel);
        Task<List<Notification>> GetNotification();
        Task<List<SMSTemplate>> GetSMSTemplate();

        Task<List<SectorOfficerMaster>> GetSectorOfficersAll();
    }
}
