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
            return await _context.SMSTemplate.OrderByDescending(d => d.Status== true && d.SMSTemplateMasterId == Convert.ToInt32(smsTemplateMasterId)).FirstOrDefaultAsync();

        }
        public async Task< List<Notification>> GetNotification()
        {
            return await _context.Notification.OrderByDescending(d=>d.NotificationId).ToListAsync();

        }
        public async Task<List<SMSTemplate>> GetSMSTemplate()
        {
            return await _context.SMSTemplate.OrderByDescending(d => d.SMSTemplateMasterId).ToListAsync();

        }


      
        public async Task<List<SectorOfficerMaster>> GetSectorOfficersAll()
        {
            return await _context.SectorOfficerMaster.ToListAsync();

        }
            public async Task<ServiceResponse> SendSMS(SMSSentModel sMSSentModel)
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
