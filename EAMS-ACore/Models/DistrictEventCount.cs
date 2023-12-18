using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class DistrictEventCount
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PartyDispatch { get; set; }
        public int PartyArrived { get; set; }
        public int SetupPollingStation { get; set; }
        public int MockPollDone { get; set; }
        public int PollStarted { get; set; }
        public int PollEnded { get; set; }
        public int MCEVMOff { get; set; }
        public int PartyDeparted { get; set; }
        public int PartyReachedAtCollection { get; set; }
        public int EVMDeposited { get; set; }
        public List<object> Children { get; set; }
    }
}
