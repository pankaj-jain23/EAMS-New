using System.ComponentModel.DataAnnotations;

namespace EAMS.ViewModels
{
    public class EventMasterViewModel
    {
        public int EventMasterId { get; set; }
        [Required(ErrorMessage = "Event Name is required")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Event Sequence is required")]
        public int EventSequence { get; set; }
        [Required(ErrorMessage = "Active Status is required")]

        public bool IsStatus { get; set; }  
    }
}
