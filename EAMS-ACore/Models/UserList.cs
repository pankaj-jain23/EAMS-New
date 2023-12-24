using EAMS_ACore.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class UserList
    {
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserType { get; set; }
        public string? UserEmail { get; set;}
        public int? StateId {  get; set; }
        public string? StateName {  get; set; }  
        public int? DistrictId {  get; set; }
        public string? DistrictName {  get; set; } 
        public int? AssemblyId {  get; set; }
        public string? AssemblyName {  get; set; }

        public List<Role> Roles { get; set; }


    }
}
