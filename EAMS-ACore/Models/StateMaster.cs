using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EAMS_ACore
{
    public class StateMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateMasterId { get; set; }

        public string StateName { get; set; } 

        public string StateCode { get; set; }

        public DateTime? StateCreatedAt { get; set; }=DateTime.UtcNow;

        public DateTime? StateUpdatedAt { get; set; }= DateTime.UtcNow;

        public DateTime? StateDeletedAt { get; set; } = DateTime.UtcNow;

        public bool StateStatus { get; set; }=true;

        public virtual List<DistrictMaster> DistrictMasters { get; set; } = new List<DistrictMaster>();
        public virtual List<AssemblyMaster> AssemblyMaster
        {
            get;
            set;
        }

        public virtual List<ParliamentConstituencyMaster> ParliamentConstituencyMaster
        {
            get;
            set;
        } 
        public virtual List<BoothMaster> BoothMaster
        {
            get;
            set;
        }
    }
}
