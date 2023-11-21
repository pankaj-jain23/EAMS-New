namespace EAMS.ViewModels
{
    public class PCViewModel
    {
        public string? PcCodeNo { get; set; }

        public string? PcName { get; set; }

        public string? PcType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool Status { get; set; } = false;
    }
}
