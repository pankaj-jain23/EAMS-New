using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class SMSTemplateViewModel
    {
       // public int SMSTemplateMasterId { get; set; }
        public string? Message { get; set; }
        public string? EntityId { get; set; }
        public string? TemplateId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
