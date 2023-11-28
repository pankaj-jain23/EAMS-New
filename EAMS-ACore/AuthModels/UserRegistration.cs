using Microsoft.AspNetCore.Identity;

namespace EAMS_ACore.AuthModels
{
    public class UserRegistration : IdentityUser
    {
        public string StateName { get; set; }
        public int StateMasterId { get; set; }
        public string DistrictName { get; set; }
        public int DistrictMasterId { get; set; }
        public string AssemblyName { get; set; }
        public int AssemblyMasterId { get; set; }
    }
}
