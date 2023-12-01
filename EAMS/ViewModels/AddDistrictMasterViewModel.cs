using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class AddDistrictMasterViewModel
    {
        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; } 
        public string StateName { get; set; }

        [Required(ErrorMessage = "District Name is required")]
        public string DistrictName { get; set; }

        [Required(ErrorMessage = "District Code is required")]
        public string DistrictCode { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool IsStatus { get; set; }
    }
}
