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

        public DateTime? DistrictCreatedAt { get; set; }= DateTime.UtcNow;

        public DateTime? DistrictUpdatedAt { get; set; }=DateTime.UtcNow;

        public DateTime? DistrictDeletedAt { get; set; } = DateTime.UtcNow;

        public bool DistrictStatus { get; set; }=true;

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
