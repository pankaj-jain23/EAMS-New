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

        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeOnly? LockTime { get; set; }
        public bool? IsLastSlot { get; set; }

        public bool? IsSucceed { get; set; }

        public string? Message { get; set; }


    }
}
