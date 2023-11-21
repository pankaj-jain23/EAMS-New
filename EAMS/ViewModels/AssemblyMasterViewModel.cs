namespace EAMS.ViewModels
{
    public class AssemblyMasterViewModel
    {
        public string AssemblyName { get; set; }
        public int AssemblyCode { get; set; }

        public string? AssemblyType { get; set; }
        public bool Status { get; set; } = true;
        public List<BoothMasterViewModel>boothMasterViewModels { get; set; }
    }
}
