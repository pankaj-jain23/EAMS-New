using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.NotificationModels
{
    public class SMSSentModel
    {
        public int? SMSTemplateMasterId { get; set; }
        public string? Message { get; set; }
        public string? Mobile { get; set; }     
        public string? RemarksFromGW { get; set; }
        public string? SentToUserType { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
