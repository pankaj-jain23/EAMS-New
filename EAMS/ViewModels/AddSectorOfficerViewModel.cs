using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class AddSectorOfficerViewModel
    {
        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; }

        [Required(ErrorMessage = "Sector officer Name is required")]
        public string SoName { get; set; }

        [Required(ErrorMessage = "Sector Officer Designation is required")]
        public string SoDesignation { get; set; }

        [Required(ErrorMessage = "Sector Officer office Name is required")]
        public string SoOfficeName { get; set; }

        [Required(ErrorMessage = "Sector Officer Assembly Code is required")]
        public int SoAssemblyCode { get; set; }

        [Required(ErrorMessage = "Sector Officer Mobile Number is required")]
        public string SoMobile { get; set; }
    }
}
