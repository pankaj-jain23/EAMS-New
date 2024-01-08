using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.HelperModels
{
    public class PollInterruptionHistoryModel
    {
        public int PollInterruptionId { get; set; }
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int? PCMasterId { get; set; }
        public string BoothName { get; set; }
        public string InterruptionReason { get; set; }
        public string? Remarks { get; set; }
        public string? OldCU { get; set; }
        public string? OldBU { get; set; }
        public string? NewCU { get; set; }
        public string? NewBU { get; set; }
        public TimeOnly? StopTime { get; set; }
        public TimeOnly? ResumeTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsPollInterrupted { get; set; }
    }
}

