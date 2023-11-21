namespace EAMS.ViewModels
{
    public class BoothMasterViewModel
    {
        public string BoothCode_No { get; set; }

        public int? TotalVoters { get; set; }

        public string? BoothName { get; set; }

        public string? BoothNoAuxy { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public short Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
