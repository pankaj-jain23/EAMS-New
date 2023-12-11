using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class SlotManagement
    {
        [Key]
        public int SlotManagementId { get; set; }
        public int StateMasterId { get; set; }
        public int EventMasterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? LockTime { get; set; }
        public bool IsLastSlot { get; set; }
        public DateTime SlotCreatedTime { get; set; } = DateTime.UtcNow;
        public ICollection<PollDetail> PollDetails { get; set; }
    }
}
