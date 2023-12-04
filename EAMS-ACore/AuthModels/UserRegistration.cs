using Microsoft.AspNetCore.Identity;

namespace EAMS_ACore.AuthModels
{
    public class UserRegistration : IdentityUser
    { 
        public int StateMasterId { get; set; } 
        public int DistrictMasterId { get; set; } 
        public int AssemblyMasterId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
