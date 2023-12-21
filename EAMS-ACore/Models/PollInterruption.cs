using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class PollInterruption
    {
        [Key]
        public int PollInterruptionId { get; set; }
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int InterruptionType { get; set; }
        public string OldCU { get; set; }
        public string OldBU { get; set; }  
        public string NewCU { get; set; }
        public string NewBU { get; set; }
        public TimeOnly? StopTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }

        public string UserId { get; set; }
        public string UserType { get; set; }
        public string UserRole { get; set; }

        public bool IsPollInterrupted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 

        public string Flag { get; set; }



    }
    public class PollInterruptionHistory
    {
        [Key]
        public int PollInterruptionHisId { get; set; }
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int InterruptionType { get; set; }
        public string OldCU { get; set; }
        public string OldBU { get; set; }
        public string NewCU { get; set; }
        public string NewBU { get; set; }
        public TimeOnly? StopTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }

        public string UserId { get; set; }
        public string UserType { get; set; }
        public string UserRole { get; set; }

        public bool IsPollInterrupted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string Flag { get; set; }



    }
}
