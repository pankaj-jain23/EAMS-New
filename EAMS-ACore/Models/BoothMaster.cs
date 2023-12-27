using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EAMS_ACore
{
    public class BoothMaster
    {
        [Key]
        public int BoothMasterId { get; set; }
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
        public int DistrictMasterId
        {
            get;
            set;
        }
        [ForeignKey("DistrictMasterId")]
        public virtual DistrictMaster? DistrictMaster
        {
            get;
            set;
        }

        public int AssemblyMasterId
        {
            get;
            set;
        }
        [ForeignKey("AssemblyMasterId")]
        public virtual AssemblyMaster? AssemblyMaster
        {
            get;
            set;
        }
        public string BoothCode_No { get; set; } 

        public int? TotalVoters { get; set; } 

        public string? BoothName { get; set; }
         
        public string? BoothNoAuxy { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool BoothStatus { get; set; }

        public DateTime? BoothCreatedAt { get; set; }  

        public DateTime? BoothUpdatedAt { get; set; }  

        public DateTime? BoothDeletedAt { get; set; } 

        public string? AssignedTo { get; set; }
        public string? AssignedBy { get; set; }  
        public DateTime? AssignedOnTime { get; set; } 
        public bool IsAssigned { get; set; }



    }
}
