using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class PollInterruptionDashboard
    {
        public int PollInterruptionMasterId { get; set; }
        public int StateMasterId { get; set; }
        
        public int DistrictMasterId { get; set; }
        public string DistrictName { get; set; }
        public int AssemblyMasterId { get; set; }
        public string AssemblyName { get; set; }
        public int BoothMasterId { get; set; }
        public string BoothCode_No { get; set; }
        
        public string BoothName { get; set; }
        public int InterruptionType { get; set; }
        public TimeOnly? StopTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? isPollInterrupted { get; set;}

    }
}
