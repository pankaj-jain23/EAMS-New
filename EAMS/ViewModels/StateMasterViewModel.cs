using EAMS.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class StateMasterViewModel
    {
        public int StateId { get; set; }

        [Required(ErrorMessage = "StateName is required")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "StateCode is required")]
        public string StateCode { get; set; }

        [Required(ErrorMessage = "IsStatus is required")]
        public bool IsStatus { get; set; }  

    }
}
