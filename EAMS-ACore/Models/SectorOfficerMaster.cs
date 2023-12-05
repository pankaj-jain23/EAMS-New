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

        public DateTime? OTPGeneratedTime { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpireTime { get; set; }
        public int OTPAttempts { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int AppPin { get; set; }
        public bool IsLocked { get; set; }

        


    }
}
