using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class AddAssemblyMasterViewModel
    {
        [Required(ErrorMessage = "Assembly Name is required")]
        public string AssemblyName { get; set; }

        [Required(ErrorMessage = "Assembly Code is required")]
        public int AssemblyCode { get; set; }

        [Required(ErrorMessage = "State Master Id is required")]
        public int StateMasterId { get; set; }

        [Required(ErrorMessage = "District Master Id is required")]
        public int DistrictMasterId { get; set; }

        //[Required(ErrorMessage = "PC Master Id is required")]
        public int PCMasterId { get; set; }

        [Required(ErrorMessage = "Assembly Type is required")]
        public string? AssemblyType { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool IsStatus { get; set; }
    }
}
