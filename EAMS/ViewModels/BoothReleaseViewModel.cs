using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class BoothReleaseViewModel
    {
        [Required(ErrorMessage = "StateMasterID is required")]
        public int StateMasterId { get; set; }

        [Required(ErrorMessage = "DistrictMasterId is required")]
        public int DistrictMasterId { get; set; }
        [Required(ErrorMessage = "AssemblyMasterId is required")]
        public int AssemblyMasterId { get; set; }
        [Required(ErrorMessage = "BoothMasterId is required")]
        public int BoothMasterId { get; set; }
        [Required(ErrorMessage = "IsAssigned is required")]
        public bool IsAssigned { get; set; }

    }
}
