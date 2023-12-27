using EAMS_ACore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAMS.ViewModels
{
    public class PollDetailViewModel
    { 
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int EventMasterId { get; set; }
        public int VotesPolled { get; set; }
        public DateTime? VotesPolledRecivedTime { get; set; }
        public int AddedBy { get; set; }
        public string UserType { get; set; }
    }
}
