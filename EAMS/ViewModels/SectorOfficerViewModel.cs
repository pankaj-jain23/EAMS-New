using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class SectorOfficerViewModel
    {
        public int SoId { get; set; }

        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; }
        [Required(ErrorMessage = "Sector Officer Name is required")]
        public string SoName { get; set; }
        [Required(ErrorMessage = "Sector Officer Designation is required")]
        public string SoDesignation { get; set; }
        [Required(ErrorMessage = "Sector Office Name is required")]
        public string SoOfficeName { get; set; }
        [Required(ErrorMessage = "Sector Officer Asembly Code is required")]
        public int SoAssemblyCode { get; set; }
        [Required(ErrorMessage = "Sector Officer Mobile is required")]
        public string SoMobile { get; set; }

        public bool? IsStatus { get; set; }
    }
}
