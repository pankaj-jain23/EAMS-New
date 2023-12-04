using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Models
{
    public class ElectionInfoMaster
    {
        [Key]
        public int ElectionInfoMasterId { get; set; }
        public int StateMasterId { get; set; }
        public int DistrictMasterId { get; set; }
        public int AssemblyMasterId { get; set; }
        public int BoothMasterId { get; set; }
        public int EventMasterId { get; set; }
        public DateTime? ElectionInfoCreatedAt { get; set; } 
        public DateTime? ElectionInfoUpdatedAt { get; set; } 
        public DateTime? ElectionInfoDeletedAt { get; set; } 
        public bool? ElectionInfoStatus { get; set; }
        public string? SOUserId { get; set; }
        public string? AROUserId { get; set; }
        public bool? IsPartyDispatched { get; set; }
        public DateTime? PartyDispatchedLastUpdate { get; set; }

        public bool? IsPartyReached { get; set; }
        public DateTime? PartyReachedLastUpdate { get; set; }

        public bool? IsSetupOfPolling { get; set; }
        public DateTime? SetupOfPollingLastUpdate { get; set; }

        public bool? IsMockPollDone { get; set; }
        public DateTime MockPollDoneLastUpdate { get; set; }

        public bool? IsPollStarted { get; set; }
        public DateTime PollStartedLastUpdate { get; set; }

        public int? FinalTVote { get; set; }
        public DateTime? VotingLastUpdate { get; set; }

        public int? VoterInQueue { get; set; }
        public DateTime? VoterInQueueLastUpdate { get; set; }
        public bool? IsPollEnded { get; set; }
        public DateTime? IsPollEndedLastUpdate { get; set; }  
        public bool? IsMCESwitchOff { get; set; }
        public DateTime? MCESwitchOffLastUpdate { get; set; }
        public bool? IsPartyDeparted { get; set; }
        public DateTime? PartyDepartedLastUpdate { get; set; }
        public bool? IsPartyReachedCollectionCenter { get; set; }
        public DateTime? PartyReachedCollectionCenterLastUpdate { get; set; }

        public bool? IsEVMDeposited { get; set; }
        public DateTime? EVMDepositedLastUpdate { get; set; }
    }
}
