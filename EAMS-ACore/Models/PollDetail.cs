using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class PollDetail
    {
        [Key]
        public int PollDetailMasterId { get; set; }
        public int SlotManagementId { get; set; }
        [ForeignKey("SlotManagementId")]
        public SlotManagement SlotManagement { get; set; }
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
