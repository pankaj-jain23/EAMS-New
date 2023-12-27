using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAMS_ACore.AuthModels
{
    public class UserRegistration : IdentityUser
    {
        public virtual List<UserState> UserStates { get; set; }
        public virtual List<UserDistrict> UserDistricts { get; set; }
        public virtual List<UserAssembly> UserAssemblies { get; set; }
        public virtual List<UserPCConstituency> UserPCConstituencies { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }

    public class UserState
    {
        [Key]
        public int UserStateId { get; set; }
        public int? StateMasterId { get; set; }
        
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual UserRegistration UserRegistration { get; set; }
    }

    public class UserDistrict
    {
        [Key]
        public int UserDistrictId { get; set; }
        public int? DistrictMasterId { get; set; }

        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual UserRegistration UserRegistration { get; set; }
    }

    public class UserAssembly
    {
        [Key]
        public int UserAssemblyId { get; set; }
        public int? AssemblyMasterId { get; set; }

        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual UserRegistration UserRegistration { get; set; }
    }

    public class UserPCConstituency
    {
        [Key]
        public int UserPCConstituencyId { get; set; }
        public int? PCMasterId { get; set; }
     
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual UserRegistration UserRegistration { get; set; }
    }

}
