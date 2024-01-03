using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EAMS.ViewModels
{
    public class SMSTemplateViewModel
    {
        
        public int? SMSTemplateMasterId { get; set; }
        public string? SMSName { get; set; }
        public string? Message { get; set; }
        public string? EntityId { get; set; }
        public string? TemplateId { get; set; }
        public bool? IsStatus { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
