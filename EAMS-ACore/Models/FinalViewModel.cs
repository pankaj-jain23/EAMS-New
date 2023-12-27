using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class FinalViewModel
    {
        public int BoothMasterId { get; set; }
        public int? TotalVoters { get; set; }
        public int? LastFinalVotesPolled { get; set; }
        public DateTime? VotesFinalPolledTime { get; set; }
 
        public bool? VoteEnabled { get; set; }
        public string? Message { get; set; }

    }
}
