namespace EAMS.ViewModels
{
    public class AssemblyMasterViewModel
    {
        public int AssemblyMasterId { get; set; }   
        public string AssemblyName { get; set; }
        public int AssemblyCode { get; set; }
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int PCMasterId { get; set; }
        public string? AssemblyType { get; set; }
        public bool Status { get; set; } = true;
        //public List<BoothMasterViewModel>boothMasterViewModels { get; set; }
    }
}
