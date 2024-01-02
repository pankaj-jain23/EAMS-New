using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EAMS_ACore
{
    public class DistrictMaster
    {
        [Key]
        public int DistrictMasterId { get; set; }
        public int StateMasterId
        {
            get;
            set;
        }
        [ForeignKey("StateMasterId")]
        public virtual StateMaster StateMaster
        {
            get;
            set;
        }
        public string DistrictName { get; set; }

        public string DistrictCode { get; set; }            

        public DateTime? DistrictCreatedAt { get; set; } 

        public DateTime? DistrictUpdatedAt { get; set; } 

        public DateTime? DistrictDeletedAt { get; set; }  

        public bool DistrictStatus { get; set; }

        [JsonIgnore]
        public virtual List<AssemblyMaster>? AssemblyMaster
        {
            get;
            set;
        }

        [JsonIgnore]
        public virtual List<BoothMaster> BoothMaster
        {
            get;
            set;
        }

    }
}
