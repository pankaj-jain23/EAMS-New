using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class CreateSOPinViewModel
    {
        [Required(ErrorMessage = "SOPin is required")]
        public int SOPin { get; set; }

        [Required(ErrorMessage = "ConfirmSOPin is required")]
        [Compare("SOPin", ErrorMessage = "The ConfirmSOPin does not match SOPin")]
        public int ConfirmSOPin { get; set; }
    }
}
