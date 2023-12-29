using EAMS_ACore.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class DashBoardProfile
    {
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserType { get; set; }
        public string? UserEmail { get; set; } 

        public List<string> Roles { get; set; }
        [JsonIgnore]
        public UserState UserStates { get; set; }
    } 
}
