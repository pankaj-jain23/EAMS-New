using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class BoothMappingViewModel
    {
        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; }
        [Required(ErrorMessage = "District Master Id is required")]
        public int DistrictMasterId { get; set; }

        [Required(ErrorMessage = "Assembly Master Id is required")]
        public int AssemblyMasterId { get; set; }
        [Required(ErrorMessage = "Booth Master Id is required")]
        public List<int> BoothMasterId { get; set; }

       // [Required(ErrorMessage = "Assigned By is required")]
        public string AssignedBy { get; set; }

        [Required(ErrorMessage = "Assigned To is required")]
        public string AssignedTo { get; set; }
        [Required(ErrorMessage = "Is Assigned Status is required")]

        public bool IsAssigned { get; set; }

    }
}
