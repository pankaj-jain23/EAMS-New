using EAMS_ACore.HelperModels;
using EAMS_ACore.NotificationModels;
using System.Threading.Tasks;

namespace EAMS_ACore.Interfaces
{
    public interface INotificationService
    {
        Task<ServiceResponse> SendNotification(Notification notificationModel);
        Task<ServiceResponse> AddSMSTemplate(SMSTemplate SMSModel);
        
        Task<SMSTemplate> GetSMSTemplateById(string smsTemplateMasterId);
        Task<ServiceResponse> SendSMS(string smsTemplateMasterId);
        Task<ServiceResponse> SendOtp(string mobile);
        Task<List<Notification>> GetNotification();
        Task<List<SMSTemplate>> GetSMSTemplate();


    }
}
