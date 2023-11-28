namespace EAMS.ViewModels
{
    public class BoothMappingViewModel
    {
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public List<int> BoothMasterId { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
        public bool IsAssigned { get; set; }

    }
}
