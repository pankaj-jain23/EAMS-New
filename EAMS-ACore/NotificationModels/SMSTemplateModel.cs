using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.NotificationModels
{
    public class SMSTemplateModel
    {
        public string? SMSName { get; set; }
        public string? Message { get; set; }
        public string? EntityId { get; set; }
        public string? TemplateId { get; set; }
        public bool? IsStatus { get; set; }
    }
}
