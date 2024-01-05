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
        public int? StateCount   { get; set; }
        public int? StateMasterId   { get; set; }
        public string? StateName { get; set; }
        public int? DistrictCount { get; set; }
        public int? DistrictMasterId { get; set; }
        public string? DistrictName { get; set; }
        public int? DistrictAssemblyCount { get; set; }
        public int? DistrictAssemblyMasterId { get; set; }
        public string? DistrictAssemblyName { get; set; }
        public int? PCCount { get; set; }
        public int? PCMasterId { get; set; }
        public string? PCName { get; set; }
        public int? PCAssemblyCount { get; set; }
        public int? PCAssemblyMasterId { get; set; }
        public string? PCAssemblyName { get; set; }

    } 
}
