using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class QueueViewModel
    {
     
        public int BoothMasterId { get; set; }
        public int? TotalVoters { get; set; }
        public int? VotesPolled { get; set; }
        public DateTime? VotesPolledTime { get; set; }
        public int? RemainingVotes { get; set; }
        public bool? VoteEnabled { get; set; }
        public string? Message { get; set; }


    }
}
