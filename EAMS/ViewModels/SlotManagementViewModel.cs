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
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? LockTime { get; set; }
        public bool IsLastSlot { get; set; }
    }
}
