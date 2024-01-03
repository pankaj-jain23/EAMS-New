using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_ACore.NotificationModels;
using EAMS_DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_DAL.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly EamsContext _context;
        public NotificationRepository(EamsContext context)
        {
            _context = context;
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
        public async Task<ServiceResponse> SendNotification(Notification notificationModel)
        {

            _context.Notification.Add(notificationModel);
            _context.SaveChanges();

            return new  ServiceResponse(){IsSucceed=true };
        }
        public async Task<ServiceResponse> AddSMSTemplate(SMSTemplate smsTemplateModel)
        {

            _context.SMSTemplate.Add(smsTemplateModel);
            _context.SaveChanges();

            return new ServiceResponse() { IsSucceed = true };
        }
        
        public async Task<SMSTemplate> GetSMSTemplateById(string smsTemplateMasterId)
        {
            return await _context.SMSTemplate.Where(d => d.Status== true && d.SMSTemplateMasterId == Convert.ToInt32(smsTemplateMasterId)).FirstOrDefaultAsync();

        }
        public async Task< List<Notification>> GetNotification()
        {
            return await _context.Notification.OrderByDescending(d=>d.NotificationId).ToListAsync();

        }
        

        public async Task<List<SMSTemplateModel>> GetSMSTemplate()
        {
            var templates = await _context.SMSTemplate.OrderByDescending(d => d.SMSTemplateMasterId).ToListAsync();

            return templates.Select(template => new SMSTemplateModel
            {
                SMSTemplateMasterId=template.SMSTemplateMasterId,
                SMSName = template.SMSName,
                Message = template.Message,
                EntityId = template.EntityId,
                TemplateId = template.TemplateId,
                IsStatus = template.Status,
                CreatedAt = template.CreatedAt,
                UpdatedAt = template.UpdatedAt
            }).ToList();

            
        }
        public async Task<ServiceResponse> UpdateSMSTemplateById(SMSTemplate sMSTemplate)
        {
            var smsTemplateMasterRecord = _context.SMSTemplate.Where(d => d.SMSTemplateMasterId == sMSTemplate.SMSTemplateMasterId).FirstOrDefault();

            if (smsTemplateMasterRecord != null)
            {
                smsTemplateMasterRecord.SMSName = sMSTemplate.SMSName;
                smsTemplateMasterRecord.Message = sMSTemplate.Message;
                smsTemplateMasterRecord.EntityId = sMSTemplate.EntityId;
                smsTemplateMasterRecord.TemplateId = sMSTemplate.TemplateId;
                smsTemplateMasterRecord.UpdatedAt = BharatDateTime();
                smsTemplateMasterRecord.Status = sMSTemplate.Status;  
                _context.SMSTemplate.Update(smsTemplateMasterRecord);
                _context.SaveChanges();
                return new ServiceResponse() { IsSucceed = true, Message= "SMS template Updated Successfully"+ sMSTemplate.SMSName };

            }
            else
            {
                return new ServiceResponse() { IsSucceed = true, Message = "SMS template Not Found" + sMSTemplate.SMSName };
            }
        }

        public async Task<List<SectorOfficerMaster>> GetSectorOfficersAll()
        {
            return await _context.SectorOfficerMaster.ToListAsync();

        }
            public async Task<ServiceResponse> SaveSMS(SMSSentModel sMSSentModel)
        {

            var smssent = ConvertToSMSSent(sMSSentModel);

            _context.SMSSent.Add(smssent);
            _context.SaveChanges();

            return new ServiceResponse() { IsSucceed = true };

        }

        private SMSSent ConvertToSMSSent(SMSSentModel sMSSentModel)
        {
            return new SMSSent
            {
                SMSTemplateMasterId = sMSSentModel.SMSTemplateMasterId,
                Message = sMSSentModel.Message,
                Mobile = sMSSentModel.Mobile,
                RemarksFromGW = sMSSentModel.RemarksFromGW,
                SentToUserType = sMSSentModel.SentToUserType,
                Status = sMSSentModel.Status,
                CreatedAt = sMSSentModel.CreatedAt
            };
        }
    }
}
