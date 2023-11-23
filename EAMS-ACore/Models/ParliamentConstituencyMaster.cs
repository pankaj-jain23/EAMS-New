using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAMS_ACore
{
    public class ParliamentConstituencyMaster
    {
        [Key]
        public int PCMasterId { get; set; }

        public int StateMasterId
        {
            get;
            set;
        }
        [ForeignKey("StateMasterId")]
        public virtual StateMaster? StateMaster
        {
            get;
            set;
        }
        public string? PcCodeNo { get; set; }

        public string? PcName { get; set; }

        public string? PcType { get; set; } 

        public DateTime? PcCreatedAt { get; set; }

        public DateTime? PcUpdatedAt { get; set; }

        public DateTime? PcDeletedAt { get; set; }

        public bool PcStatus { get; set; }
        public virtual List<AssemblyMaster>? AssemblyMaster
        {
            get;
            set;
        }
    }
}
