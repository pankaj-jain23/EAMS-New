using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class PCViewModel
    {
        [Required(ErrorMessage = "PC Code is required")]
        public string? PcCodeNo { get; set; }
        [Required(ErrorMessage = "PC Name is required")]
        public string? PcName { get; set; }
        [Required(ErrorMessage = "PC Type is required")]
        public string? PcType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
        [Required(ErrorMessage = "PC Status is required")]
        public bool IsStatus { get; set; } 
    }
}
