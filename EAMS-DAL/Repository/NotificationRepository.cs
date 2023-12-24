using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
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
        public async Task< List<Notification>> GetNotification()
        {
            return await _context.Notification.ToListAsync();

        }
    }
}
