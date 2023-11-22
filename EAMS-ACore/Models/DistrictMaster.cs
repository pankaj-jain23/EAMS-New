﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EAMS_ACore
{
    public class DistrictMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
                
        public virtual List<AssemblyMaster>? AssemblyMaster
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