using System.ComponentModel.DataAnnotations;

namespace EAMS.AuthViewModels
{
    public class UserRegistrationViewModel
    {
        public List<StateViewModel> UserStates { get; set; }

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
    public class StateViewModel
    {
        public int StateMasterId { get; set; } 
        public List<DistrictViewModel> Districts { get; set; }
        public List<PCConstituencyViewModel> PCConstituencies { get; set; }
    }

    public class DistrictViewModel
    {
        public int DistrictMasterId { get; set; } 
        public List<AssemblyViewModel> Assemblies { get; set; }
    }
    public class PCConstituencyViewModel
    {
        public int PCMasterId { get; set; }
        public List<AssemblyViewModel> Assemblies { get; set; }
    }
    public class AssemblyViewModel
    {
        public int AssemblyMasterId { get; set; } 
    }

 
}
