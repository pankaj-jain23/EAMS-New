using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class SectorOfficerMaster
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SOMasterId { get; set; }
        public int StateMasterId
        {
            get;
            set;
        }
       
        public string SoName { get; set; }

        public string SoDesignation { get; set; }

        public string SoOfficeName { get; set; }
        public int SoAssemblyCode { get; set; }
        public string SoMobile { get; set; }

        public DateTime? SoCreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? SOUpdatedAt { get; set; } = DateTime.UtcNow;
        public bool SoStatus { get; set; }
        
    }
}
