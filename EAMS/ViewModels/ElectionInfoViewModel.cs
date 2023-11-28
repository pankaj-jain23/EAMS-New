namespace EAMS.ViewModels
{
    public class ElectionInfoViewModel
    {
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int EventMasterId { get; set; }
        public bool EventStatus { get; set; }
        //public string? SOUserId { get; set; }
        //public string? AROUserId { get; set; }
    }
}
