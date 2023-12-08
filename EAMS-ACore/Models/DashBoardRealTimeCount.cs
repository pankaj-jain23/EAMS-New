using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class DashBoardRealTimeCount
    {
        public int Total { get; set; } 
        public List<EventCount> Events { get; set; }
    }

    public class EventCount
    {
        public string EventName { get; set; }
        public int Count { get; set; }
    }
}
