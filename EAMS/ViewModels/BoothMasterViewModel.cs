namespace EAMS.ViewModels
{
    public class BoothMasterViewModel
    {
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }

        public int BoothMasterId { get; set; }
        
        public string BoothCode_No { get; set; }

        public int? TotalVoters { get; set; }

        public string? BoothName { get; set; }

        public string? BoothNoAuxy { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool BoothStatus { get; set; }

        

       
    }
}
