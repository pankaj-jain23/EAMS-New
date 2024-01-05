using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.HelperModels
{
    public class CombinedMaster
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public bool DistrictStatus { get; set; }
        public string DistrictCode { get; set; }

        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public int AssemblyCode { get; set; }

        public int soMasterId { get; set; }
        public string soName { get; set; }
        public string soMobile { get; set; }
        public string BoothCode_No { get; set; }

        public int BoothMasterId { get; set; }
        public string BoothName { get; set; }

        public int PCMasterId { get; set; }
        public string PCName { get; set; }
        public string BoothAuxy { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsStatus { get; set; }
    }
}

