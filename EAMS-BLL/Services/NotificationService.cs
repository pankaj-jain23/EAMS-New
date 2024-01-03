using EAMS.Helper;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.NotificationModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;
namespace EAMS_BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings, INotificationRepository notificationRepository)
        {
            _fcmNotificationSetting = settings.Value;
            _notificationRepository = notificationRepository;
        }
        private DateTime? BharatDateTime()
        {
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            TimeSpan istOffset = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);
            TimeZoneInfo istTimeZone = TimeZoneInfo.CreateCustomTimeZone("IST", istOffset, "IST", "IST");
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);

            return DateTime.SpecifyKind(hiINDateTime, DateTimeKind.Utc);
        }
        public async Task<List<Notification>> GetNotification()
        {
            return await _notificationRepository.GetNotification();
        }

        public async Task<ServiceResponse> SendNotification(Notification notificationModel)
        {
            return await _notificationRepository.SendNotification(notificationModel);
        }
        public async Task<ServiceResponse> AddSMSTemplate(SMSTemplate SMSModel)
        {
            return await _notificationRepository.AddSMSTemplate(SMSModel);
        }
        public async Task<SMSTemplate> GetSMSTemplateById(string smsTemplateMasterId)
        {
            return await _notificationRepository.GetSMSTemplateById(smsTemplateMasterId);

        }
        public async Task<List<SMSTemplateModel>> GetSMSTemplate()
        {
            return await _notificationRepository.GetSMSTemplate();
        }
        public async Task<ServiceResponse> UpdateSMSTemplateById(SMSTemplate sMSTemplate)
        {
            return await _notificationRepository.UpdateSMSTemplateById(sMSTemplate);
        }
        
        public async Task<ServiceResponse> SendOtp(string mobile, string otp)
        {

            SMSSentModel sMSSentModel = new SMSSentModel(); int sent = 0; int Notsent = 0;
            string userNameSMS = SMSEnum.UserName.GetStringValue();
            string Password = SMSEnum.Password.GetStringValue();
            string senderId = SMSEnum.SenderId.GetStringValue();
            string entityId = SMSEnum.EntityId.GetStringValue();

            string FinalsmsTemplateMsg = "";
            string templateId = "1407168318422038309";
            string userName = "Chetna"; string placeholder = "{#var#}";
            //var template = "Dear {#var#},eOffice Services has been resumed now - O/o DGR";

           // string template = "Punjab Bye Elections 2023 to 04-Jalandhar(SC) PC, Dear Sector Officer, Your OTP for registration on PPDMS Mobile App is {#var#}. -CEOPJB";
            string template = "Punjab Bye Elections 2023 to 04-Jalandhar(SC) PC, Dear Sector Officer, Your OTP for registration on PPDMS Mobile App is " + Convert.ToString(otp).Trim() + ". -CEOPJB";
            if (template.Contains(placeholder))
            {

                FinalsmsTemplateMsg = template.Replace(placeholder, Convert.ToString(otp).Trim());

            }
            else
            {
                FinalsmsTemplateMsg = template;
            }


            var result = SendSMSAsync(userNameSMS, Password, senderId, mobile, FinalsmsTemplateMsg, entityId, templateId);


            if (result.Result.Contains("Message Accepted"))
            {

                sent += 1;
            }
            else
            {
                Notsent += 0;
            }

            //sMSSentModel = new SMSSentModel()
            //{
            //    SMSTemplateMasterId = smsTemplateRecord.SMSTemplateMasterId,
            //    Message = FinalsmsTemplateMsg,
            //    Mobile = soDetail.SoMobile,
            //    RemarksFromGW = result.Result,
            //    CreatedAt = BharatDateTime(),
            //    //Status=,
            //    //SentToUserType=

            //};

            //var res = _notificationRepository.SendSMS(sMSSentModel);

            return new ServiceResponse()
            {
                IsSucceed = true,
                Message = "SMS Sent: " + sent + "/Not Sent: " + Notsent
            };

        }

        /*public async Task<ServiceResponse> SendOtp(string mobile,string otp)
        {
            
            SMSSentModel sMSSentModel = new SMSSentModel(); int sent = 0; int Notsent = 0;
            string userNameSMS = SMSEnum.UserName.GetStringValue();
            string Password = SMSEnum.Password.GetStringValue();
            string senderId = SMSEnum.SenderId.GetStringValue();
            string entityId = SMSEnum.EntityId.GetStringValue();
            string SMSTypeOTP = SMSEnum.OTP.GetStringValue();
            string FinalsmsTemplateMsg = "";string placeholder = "{#var#}";

            var getTemplate = _notificationRepository.GetSMSTemplateById(SMSTypeOTP);
            string template = getTemplate.Result.Message;


            if (template.Contains(placeholder))
            {

                FinalsmsTemplateMsg = template.Replace(placeholder, Convert.ToString(otp).Trim());

            }
            else
            {
                FinalsmsTemplateMsg = template;
            }


            var result = SendSMSAsync(userNameSMS, Password, senderId, mobile, FinalsmsTemplateMsg, entityId, getTemplate.Result.TemplateId.ToString());
           
            if (result.Result.Contains(SMSEnum.MessageAccepted.GetStringValue()))
            {

                sent += 1;
            }
            else
            {
                Notsent += 0;
            }

            sMSSentModel = new SMSSentModel()
            {
                SMSTemplateMasterId = getTemplate.Result.SMSTemplateMasterId,
                Message = FinalsmsTemplateMsg,
                Mobile = mobile,
                RemarksFromGW = result.Result,
                CreatedAt = BharatDateTime(),
                //Status=,
                SentToUserType="SO"

            };

            var res = _notificationRepository.SaveSMS(sMSSentModel);

            return new ServiceResponse() {
                IsSucceed=true,
                Message = "SMS Sent: " + sent + "/Not Sent: " + Notsent
            };
          
        }*/
        public async Task<ServiceResponse> SendSMS(string smsTemplateMasterId)
        {
            SMSSentModel sMSSentModel = new SMSSentModel(); int sent = 0; int Notsent = 0;
            var soRecord = await _notificationRepository.GetSectorOfficersAll();
            string FinalsmsTemplateMsg = "";
            var smsTemplateRecord = await _notificationRepository.GetSMSTemplateById(smsTemplateMasterId);
            if (soRecord.Count > 0)
            {
                if (smsTemplateRecord is not null)
                {
                    string userNameSMS = SMSEnum.UserName.GetStringValue();
                    string Password = SMSEnum.Password.GetStringValue();
                    string senderId = SMSEnum.SenderId.GetStringValue();
                    string entityId = SMSEnum.EntityId.GetStringValue();
                    string MessageDb = smsTemplateRecord.Message;
                    string mobile = ""; string userName = "";
                    string userNamePlaceholder = "{#userName#}";
                                     

                    foreach (var soDetail in soRecord)
                    {
                        userName = soDetail.SoName; mobile=soDetail.SoMobile;
                        if (smsTemplateRecord.Message.Contains(userNamePlaceholder))
                        {

                            FinalsmsTemplateMsg = smsTemplateRecord.Message.Replace(userNamePlaceholder, userName);

                        }
                        else
                        {
                            FinalsmsTemplateMsg = smsTemplateRecord.Message;
                        }
                        
                        
                       var result = SendSMSAsync(userNameSMS, Password, senderId, mobile, FinalsmsTemplateMsg, entityId, smsTemplateMasterId);
                        if (result.Result.Contains(SMSEnum.MessageAccepted.GetStringValue()))
                        {

                            sent += 1;
                        }
                        else
                        {
                            Notsent += 0;
                        }
                        
                            sMSSentModel = new SMSSentModel()
                        {
                            SMSTemplateMasterId = smsTemplateRecord.SMSTemplateMasterId,
                            Message = FinalsmsTemplateMsg,
                            Mobile = soDetail.SoMobile,
                            RemarksFromGW= result.Result,
                            CreatedAt=BharatDateTime(),
                            //Status=,
                            //SentToUserType=

                        };

                     var res= _notificationRepository.SaveSMS(sMSSentModel);


                    }
                }

            }

            return new ServiceResponse() { Message = "SMS Sent: "+sent+"/Not Sent: "+Notsent };
        }


        #region SendSMS
        public async Task<string> SendSMSAsync(string uname, string password, string senderidstr, string mobileNo, string message, string entityidstr, string templateidstr)
        {
            string mob1 = mobileNo?.Trim() ?? string.Empty;
            string username = uname?.Trim() ?? string.Empty;
            string pin = password?.Trim() ?? string.Empty;
            string senderid = senderidstr?.Trim() ?? string.Empty;
            string entityid = entityidstr?.Trim() ?? string.Empty;
            string templateid = templateidstr?.Trim() ?? string.Empty;
            string msg = message?.Trim() ?? string.Empty;

            string SMSPostUrl = "https://smsgw.sms.gov.in/failsafe/MLink?username=##username##&pin=##pin##&message=##message##&mnumber=##mnumber##&signature=##senderid##&dlt_entity_id=##dlt_entity_id##&dlt_template_id=##dlt_template_id##";

            if (!string.IsNullOrEmpty(msg))
            {
                Check_SSL_Certificate();
                string req = SMSPostUrl.Replace("&amp;", "&")
                                        .Replace("##username##", username)
                                        .Replace("##pin##", pin)
                                        .Replace("##senderid##", senderid)
                                        .Replace("##mnumber##", mob1)
                                        .Replace("##dlt_entity_id##", entityid)
                                        .Replace("##dlt_template_id##", templateid)
                                        .Replace("##message##", msg);

                try
                {
                    using (HttpClient client = CreateHttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(req);

                        response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status code is not success.

                        string responseContent = await response.Content.ReadAsStringAsync();

                        return responseContent;
                    }
                }
                catch (HttpRequestException ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return string.Empty;
            }
        }


        public static void Check_SSL_Certificate()
        {
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidate;
        }

        public static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
            {
                return true;
            }
            else if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                // You can adapt the zone logic if needed
                if ((sender as HttpRequestMessage)?.RequestUri.IsLoopback ?? false)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                    RemoteCertificateValidate(sender, certificate, chain, sslPolicyErrors)
            };

            return new HttpClient(handler);
        }
        #endregion
    }
}
