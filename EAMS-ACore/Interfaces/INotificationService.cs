using EAMS.Helper;
using EAMS_ACore.HelperModels;
using EAMS_ACore.NotificationModels;
using System.Threading.Tasks;

namespace EAMS_ACore.Interfaces
{
    public interface INotificationService
    {
        Task<ServiceResponse> SendNotification(Notification notificationModel);
        Task<ServiceResponse> AddSMSTemplate(SMSTemplate SMSModel);
        Task<ServiceResponse> UpdateSMSTemplateById(SMSTemplate sMSTemplate);
        Task<SMSTemplate> GetSMSTemplateById(string smsTemplateMasterId);
        Task<ServiceResponse> SendSMS(string smsTemplateMasterId);
        Task<ServiceResponse> SendOtp(string mobile,string otp);
        Task<List<Notification>> GetNotification();
        Task<List<SMSTemplateModel>> GetSMSTemplate();


    }
}
