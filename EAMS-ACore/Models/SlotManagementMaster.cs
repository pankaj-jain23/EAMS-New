using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class SlotManagementMaster
    {
        [Key]
        public int SlotManagementId { get; set; }
        public int StateMasterId { get; set; }
        public int EventMasterId { get; set; }
        public int SlotSequenceNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public TimeOnly? LockTime { get; set; }
        public bool IsLastSlot { get; set; }
        public DateTimeOffset SlotCreatedTime { get; set; } = DateTimeOffset.Now;
         
            
    }
    
}
