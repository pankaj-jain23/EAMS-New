using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class ElectionInfoViewModel
    {
        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; }
        [Required(ErrorMessage = "District Master Id is required")]
        public int DistrictMasterId { get; set; }
        [Required(ErrorMessage = "Assembly Master Id is required")]
        public int AssemblyMasterId { get; set; }

        [Required(ErrorMessage = "Booth Master Id is required")]
        public int BoothMasterId { get; set; }
        [Required(ErrorMessage = "Event Master Id is required")]
        public int EventMasterId { get; set; }

        [Required(ErrorMessage = "Event Status is required")]
        public bool EventStatus { get; set; }
        //public string? SOUserId { get; set; }
        //public string? AROUserId { get; set; }
    }
}
