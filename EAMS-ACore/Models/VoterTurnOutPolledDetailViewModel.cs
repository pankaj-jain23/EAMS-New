using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class VoterTurnOutPolledDetailViewModel
    {
        public int StateMasterId { get; set; }
       
        public int DistrictMasterId { get; set; }
       
        public int AssemblyMasterId { get; set; }

       
        public int BoothMasterId { get; set; }
       
        
        public int? TotalVoters { get; set; }
        public int VotesPolled { get; set; }
        public DateTime? VotesPolledRecivedTime { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? LockTime { get; set; }
        public bool? IsLastSlot { get; set; }

        public bool? MethodResponse { get; set; }
        public string? Message { get; set; }


    }
}
