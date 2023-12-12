using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class VoterTurnOutPolledDetailViewModel
    {
        [JsonIgnore]
        public int StateMasterId { get; set; }
        [JsonIgnore]
        public int DistrictMasterId { get; set; }
        [JsonIgnore]
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int? TotalVoters { get; set; }
        public int VotesPolled { get; set; }
        public DateTime? VotesPolledRecivedTime { get; set; }        
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeOnly? LockTime { get; set; }
        public bool? IsLastSlot { get; set; }
        [JsonIgnore]
        public bool? IsSucceed { get; set; }

        public string? Message { get; set; }


    }
}
