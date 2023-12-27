using System.ComponentModel.DataAnnotations;

namespace EAMS.AuthViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "StateMasterId is required")]
        public List<string> StateMasterId { get; set; }

        public List<string> DistrictMasterId { get; set; }
        public List<string> PCMasterId { get; set; }

        [Required(ErrorMessage = "AssemblyMasterId is required")]
        public List<string> AssemblyMasterId { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public List<string>? RoleId
        {
            get;
            set;
        }
    }
}
