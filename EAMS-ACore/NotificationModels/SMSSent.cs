using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.NotificationModels
{
    public class SMSSent
    {
        [Key]
        public int SMSSentMasterId { get; set; }
        public int? SMSTemplateMasterId
        {
            get;
            set;
        }
        [ForeignKey("SMSTemplateMasterId")]
        public virtual SMSTemplate SMSTemplate
        {
            get;
            set;
        }
      public  string Mobile { get; set; }
        public string? Message { get; set; }
        public string? RemarksFromGW { get; set; }
        public string? SentToUserType { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
      
    }
}
