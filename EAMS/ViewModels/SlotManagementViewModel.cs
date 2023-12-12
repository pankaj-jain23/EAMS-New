namespace EAMS.ViewModels
{
    public class SlotManagementViewModel
    {
        public int StateMasterId { get; set; }
        public int EventMasterId { get; set; }
        public List<SlotTimeViewModel> slotTimes { get; set; }
      
    }
    public class SlotTimeViewModel
    {
        public int SlotSequenceNumber { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? LockTime { get; set; }
        public bool IsLastSlot { get; set; }
    }
}
