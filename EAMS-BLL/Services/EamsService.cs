using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_DAL.AuthRepository;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EAMS_BLL.Services
{
    public class EamsService : IEamsService
    {
        private readonly IEamsRepository _eamsRepository;
        private readonly IAuthRepository _authRepository;
        public EamsService(IEamsRepository eamsRepository, IAuthRepository authRepository)
        {
            _eamsRepository = eamsRepository;
            _authRepository = authRepository;
        }
        private DateTime? BharatDateTime()
        {
            DateTime dateTime = DateTime.Now;
            DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
            TimeSpan istOffset = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);
            TimeZoneInfo istTimeZone = TimeZoneInfo.CreateCustomTimeZone("IST", istOffset, "IST", "IST");
            DateTime hiINDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);

            return DateTime.SpecifyKind(hiINDateTime, DateTimeKind.Utc);
        }
        #region State Master

        public Task<List<StateMaster>> GetState()
        {
            return _eamsRepository.GetState();
        }

        public async Task<Response> UpdateStateById(StateMaster stateMaster)
        {
            return await _eamsRepository.UpdateStateById(stateMaster);
        }
        public async Task<Response> AddState(StateMaster stateMaster)
        {
            return await _eamsRepository.AddState(stateMaster);

        }

        #endregion

        #region District Master

        public Task<List<CombinedMaster>> GetDistrictById(string stateMasterId)
        {
            return _eamsRepository.GetDistrictById(stateMasterId);
        }


        public async Task<Response> UpdateDistrictById(DistrictMaster districtMaster)
        {
            return await _eamsRepository.UpdateDistrictById(districtMaster);

        }

        public async Task<Response> AddDistrict(DistrictMaster districtMaster)
        {
            return await _eamsRepository.AddDistrict(districtMaster);
        }
        #endregion   

        #region Assembly  Master
        public Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId)
        {
            return _eamsRepository.GetAssemblies(stateId, districtId);
        }

        public async Task<Response> UpdateAssembliesById(AssemblyMaster assemblyMaster)
        {
            return await _eamsRepository.UpdateAssembliesById(assemblyMaster);
        }

        public async Task<Response> AddAssemblies(AssemblyMaster assemblyMaster)
        {
            return await _eamsRepository.AddAssemblies(assemblyMaster);
        }
        #endregion

        #region  SO Master
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return await _eamsRepository.GetSectorOfficersListById(stateMasterId, districtMasterId, assemblyMasterId);
        }
        public async Task<SectorOfficerProfile> GetSectorOfficerProfile(string soId)
        {
            return await _eamsRepository.GetSectorOfficerProfile(soId);
        }
        public async Task<Response> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster)
        {
            return await _eamsRepository.AddSectorOfficer(sectorOfficerMaster);
        }

        public async Task<Response> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster)
        {
            return await _eamsRepository.UpdateSectorOfficer(sectorOfficerMaster);
        }
        public async Task<List<CombinedMaster>> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId)
        {
            return await _eamsRepository.GetBoothListBySoId(stateMasterId, districtMasterId, assemblyMasterId, soId);
        }

        #endregion

        #region Booth Master

        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return await _eamsRepository.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);
        }
        public async Task<Response> AddBooth(BoothMaster boothMaster)
        {
            return await _eamsRepository.AddBooth(boothMaster);

        }

        public async Task<Response> UpdateBooth(BoothMaster boothMaster)
        {
            return await _eamsRepository.UpdateBooth(boothMaster);
        }
        public async Task<Response> BoothMapping(List<BoothMaster> boothMaster)
        {
            return await _eamsRepository.BoothMapping(boothMaster);
        }
        public async Task<Response> ReleaseBooth(BoothMaster boothMaster)
        {
            return await _eamsRepository.ReleaseBooth(boothMaster);
        }

        #endregion

        #region Event Master
        public Task<List<EventMaster>> GetEventList()
        {
            return _eamsRepository.GetEventList();
        }


        public async Task<Response> UpdateEventById(EventMaster eventMaster)
        {
            return await _eamsRepository.UpdateEventById(eventMaster);
        }
        public async Task<List<EventWiseBooth>> GetBoothListByEventId(string eventId, string soId)
        {
            return await _eamsRepository.GetBoothListByEventId(eventId, soId);
        }
        public async Task<List<EventWiseBooth>> GetBoothStatusforARO(string assemblyMasterId, string boothMasterId)
        {
            return await _eamsRepository.GetBoothStatusforARO(assemblyMasterId, boothMasterId);
        }

        #endregion

        #region PCMaster
        public Task<List<ParliamentConstituencyMaster>> GetPCList()
        {
            return _eamsRepository.GetPCList();
        }
        #endregion

        #region EventActivity

        public async Task<Response> EventActivity(ElectionInfoMaster electionInfoMaster)
        {
            var electionInfoRecord = await _eamsRepository.EventUpdationStatus(electionInfoMaster);
            if (electionInfoRecord != null)
            {
                switch (electionInfoMaster.EventMasterId)
                {
                    case 1: //party Dispatch

                        if (electionInfoRecord.IsPartyReached == false || electionInfoRecord.IsPartyReached == null)
                        {
                            if (electionInfoRecord.IsPartyDispatched is not null)
                            {
                                electionInfoRecord.IsPartyDispatched = electionInfoMaster.IsPartyDispatched;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                //electionInfoRecord.PartyDispatchedLastUpdate = _eamsRepository.BharatDateTime();
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {
                                //Already Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Already Dispatched." };

                            }
                        }
                        else
                        {
                            // party alteady arrived, cnt change status!
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Has Already Arrived." };
                        }
                    case 2:
                        if (electionInfoRecord.IsPartyDispatched == true)
                        {

                            if (electionInfoRecord.IsSetupOfPolling == false || electionInfoRecord.IsSetupOfPolling == null)
                            {
                                electionInfoRecord.IsPartyReached = electionInfoMaster.IsPartyReached;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, SetUpPolling Already yes." };

                            }


                        }
                        else
                        {

                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Dispatched Yet." };
                        }
                    case 3:
                        if (electionInfoRecord.IsPartyReached == true)
                        {

                            if (electionInfoRecord.IsMockPollDone == false || electionInfoRecord.IsMockPollDone == null)
                            {
                                electionInfoRecord.IsSetupOfPolling = electionInfoMaster.IsSetupOfPolling;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, MockPoll Already yes." };
                            }



                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Arrived Yet." };

                        }
                    case 4:
                        if (electionInfoRecord.IsSetupOfPolling == true) // mockpoll event 4th event
                        {

                            if (electionInfoRecord.IsPollStarted == false || electionInfoRecord.IsPollStarted == null)
                            {
                                electionInfoRecord.IsMockPollDone = electionInfoMaster.IsMockPollDone;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Poll Started Already yes." };
                            }



                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Set Up Of Polling Not Done Yet." };

                        }

                    case 5:// poll started 5th event
                        if (electionInfoRecord.IsMockPollDone == true)  // prev event
                        {
                            //check polled detail for voter turn out, if enetered then cant change poll started status

                            var pollCanStart = _eamsRepository.CanPollStart(electionInfoRecord.BoothMasterId, 6);
                            if (pollCanStart == true)
                            {
                                electionInfoRecord.IsPollStarted = electionInfoMaster.IsPollStarted;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Voter Turn Out Status is Entered." };

                            }
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Mock Poll Done is not Done Yet." };

                        }

                    case 7:
                        //queue
                        //check only voter turn out entered or not, but not check last entered value

                        var QueueCanStart = _eamsRepository.CanQueueStart(electionInfoRecord.BoothMasterId);
                        if (QueueCanStart == true)
                        {
                            bool queueTime = _eamsRepository.QueueTime(electionInfoRecord.BoothMasterId);
                            if (queueTime == true)
                            {

                                if (electionInfoRecord.FinalTVote == null)  // next event
                                {

                                    if (electionInfoMaster.VoterInQueue != null)
                                    {

                                        if (electionInfoRecord.VoterInQueue == null)
                                        {
                                            Queue fetchResult = await _eamsRepository.GetTotalRemainingVoters(electionInfoMaster.BoothMasterId.ToString());
                                            if (electionInfoMaster.VoterInQueue <= fetchResult.TotalVoters)
                                            {

                                                if (electionInfoMaster.VoterInQueue <= fetchResult.RemainingVotes)
                                                {
                                                    electionInfoRecord.VoterInQueue = electionInfoMaster.VoterInQueue;
                                                    electionInfoRecord.IsVoterTurnOut = true;
                                                    electionInfoRecord.VotingTurnOutLastUpdate = BharatDateTime();
                                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                                    return await _eamsRepository.EventActivity(electionInfoRecord);
                                                }
                                                else
                                                {
                                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Voters in queue cannot exceed voter remaining!" };

                                                }
                                            }

                                            else
                                            {

                                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Polling should not be more than Total Voters!" };
                                            }
                                            // VotesPolled.
                                            //RemainingVoters 
                                            //queue_voters
                                            //electionInfoRecord.VoterInQueue = electionInfoMaster.VoterInQueue;
                                            //electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                            return await _eamsRepository.EventActivity(electionInfoRecord);
                                        }
                                        else
                                        {
                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Queue value Already Entered. Pls proceed for the Final Voting value" };
                                        }

                                    }
                                    else
                                    {

                                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Queue value cannot be null" };
                                    }
                                }
                                else
                                {

                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Queue has been Freezed as Final Vote has been entered." };
                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Queue will be Opened at specified Time." };
                            }
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Voter Turn Out Not Entered any Values." };

                        }


                    case 8:
                        // Final Votes
                        if (electionInfoRecord.VoterInQueue != null) //check queue
                        {
                            if (electionInfoRecord.IsPollEnded == false || electionInfoRecord.IsPollEnded == null)
                            {
                                //change final voting 
                                if (electionInfoMaster.FinalTVote != null && electionInfoMaster.FinalTVote > 0)
                                {
                                    // one more check that last votes polled value and final vote now should not be greater than total voters

                                    Queue fetchResult = await _eamsRepository.GetTotalRemainingVoters(electionInfoMaster.BoothMasterId.ToString());
                                    if (electionInfoMaster.FinalTVote <= fetchResult.TotalVoters) //
                                    {
                                        if (electionInfoMaster.FinalTVote >= fetchResult.VotesPolled)// shoulbe greater n equal to last polled votes 
                                        {
                                            electionInfoRecord.FinalTVote = electionInfoMaster.FinalTVote;
                                            electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                            return await _eamsRepository.EventActivity(electionInfoRecord);
                                        }
                                        else
                                        {

                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Final Votes Cannot be less than Last Votes Polled" };

                                        }

                                    }
                                    else
                                    {
                                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Final Votes Cannot be Greater than Total Voters" };
                                    }
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Final Votes Cannot be Null or 0." };
                                }



                            }
                            else
                            {
                                // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Poll Ended Already." };

                            }

                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Voter in queue is not updated Yet." };

                        }

                    case 9:
                        if (electionInfoRecord.FinalTVote > 0) // Poll ended--
                        {
                            if (electionInfoRecord.IsMCESwitchOff == false || electionInfoRecord.IsMCESwitchOff == null)
                            {
                                if (electionInfoRecord.IsPollEnded == false || electionInfoRecord.IsPollEnded == null)
                                {
                                    electionInfoRecord.IsPollEnded = electionInfoMaster.IsPollEnded;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoRecord);
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Poll Already Ended." };

                                }
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Machine Closed & EVM Switched Off Already Yes." };
                            }



                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Final Votes not Done yet." };

                        }

                    case 10:
                        if (electionInfoRecord.IsPollEnded == true) // Machine Switch Off and EVM Cleared
                        {
                            if (electionInfoRecord.IsMCESwitchOff == false || electionInfoRecord.IsMCESwitchOff == null)
                            {
                                if (electionInfoRecord.IsPartyDeparted == false || electionInfoRecord.IsMCESwitchOff == null)
                                {
                                    electionInfoRecord.IsMCESwitchOff = electionInfoMaster.IsMCESwitchOff;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoRecord);
                                }
                                else
                                {

                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Party Already Departed." };
                                }

                            }
                            else
                            {
                                // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Machine Closed and EVM Switched Off Already yes." };

                            }

                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Poll Is Not Ended yet." };

                        }

                    case 11:
                        if (electionInfoRecord.IsMCESwitchOff == true) // party departed
                        {
                            if (electionInfoRecord.IsPartyDeparted == false || electionInfoRecord.IsPartyDeparted == null)
                            {
                                if (electionInfoRecord.IsPartyReachedCollectionCenter == false || electionInfoRecord.IsPartyReachedCollectionCenter == null)
                                {
                                    electionInfoRecord.IsPartyDeparted = electionInfoMaster.IsPartyDeparted;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoRecord);
                                }
                                else
                                {

                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Party Already Reached at Collection Centre." };
                                }

                            }
                            else
                            {
                                // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Departed Already Yes." };

                            }

                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Machine Closed & EVM Not Switched Off yet." };

                        }

                    case 12:
                        if (electionInfoRecord.IsPartyDeparted == true)
                        {
                            if (electionInfoRecord.IsPartyReachedCollectionCenter == false || electionInfoRecord.IsPartyReachedCollectionCenter == null)
                            {
                                if (electionInfoRecord.IsEVMDeposited == false || electionInfoRecord.IsEVMDeposited == null)
                                {
                                    electionInfoRecord.IsPartyReachedCollectionCenter = electionInfoMaster.IsPartyReachedCollectionCenter;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoRecord);
                                }
                                else
                                {

                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, EVM Deposited." };
                                }

                            }
                            else
                            {
                                // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Arrived Already Yes." };

                            }

                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Is Not Departed yet." };

                        }

                    case 13:
                        if (electionInfoRecord.IsPartyReachedCollectionCenter == true) // Machine Switch Off and EVM Cleared
                        {

                            if (electionInfoRecord.IsEVMDeposited == false || electionInfoRecord.IsEVMDeposited == null)
                            {
                                electionInfoRecord.IsEVMDeposited = electionInfoMaster.IsEVMDeposited;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoRecord);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, EVM Already Deposited." };
                            }


                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Is Not Departed yet." };

                        }

                }
            }
            else if (electionInfoRecord == null)
            {
                return await _eamsRepository.EventActivity(electionInfoMaster);

            }

            return new Response { Status = RequestStatusEnum.BadRequest, Message = "something went wrong" };

        }

        public async Task<List<EventWiseBoothStatus>> EventWiseBoothStatus(string soId)
        {
            return await _eamsRepository.EventWiseBoothStatus(soId);
        }

        public async Task<VoterTurnOutPolledDetailViewModel> GetLastUpdatedPollDetail(string boothMasterId, int eventid)

        {
            return await _eamsRepository.GetLastUpdatedPollDetail(boothMasterId, eventid);

        }
        public async Task<Queue> GetVoterInQueue(string boothMasterId)

        {
            return await _eamsRepository.GetVoterInQueue(boothMasterId);

        }
        public async Task<FinalViewModel> GetFinalVotes(string boothMasterId)

        {
            return await _eamsRepository.GetFinalVotes(boothMasterId);

        }
        public async Task<Response> AddVoterTurnOut(string boothMasterId, int eventid, string voterValue)
        {
            return await _eamsRepository.AddVoterTurnOut(boothMasterId, eventid, voterValue);

        }



        public async Task<List<EventActivityCount>> GetEventListDistrictWiseById(string stateId)
        {
            return await _eamsRepository.GetEventListDistrictWiseById(stateId);
        }
        public async Task<List<EventActivityCount>> GetEventListAssemblyWiseById(string stateId, string districtId)
        {
            return await _eamsRepository.GetEventListAssemblyWiseById(stateId, districtId);
        }
        public async Task<List<EventActivityBoothWise>> GetEventListBoothWiseById(string stateId, string districtId, string assemblyId)
        {
            return await _eamsRepository.GetEventListBoothWiseById(stateId, districtId, assemblyId);
        }
        #endregion

        #region SendDashBoardCount 
        public async Task<DashBoardRealTimeCount> GetDashBoardCount()
        {
            return await _eamsRepository.GetDashBoardCount();
        }


        #endregion

        #region SlotManagement
        public Task<Response> AddEventSlot(List<SlotManagementMaster> addEventSlot)
        {
            return _eamsRepository.AddEventSlot(addEventSlot);
        }

        public Task<List<SlotManagementMaster>> GetEventSlotList()
        {
            return _eamsRepository.GetEventSlotList();
        }
        #endregion

        #region UserList
        public async Task<List<UserList>> GetUserList(string userName, string type)
        {
            List<UserList> list = new List<UserList>();
            if (type == "SO") 
            {

                return list = await _eamsRepository.GetUserList(userName, type);
  

            }
            else if (type == "ARO")
            {

                var aroUsers = await _authRepository.FindUserListByName(userName);
                foreach (var user in aroUsers)
                {
                    var mappedUser = new UserList
                    {
                        Name = user.UserName,
                        MobileNumber = user.PhoneNumber
                        // Map other properties as needed
                    };

                    list.Add(mappedUser);
                }

                return list;

            }

            return list.ToList();
        }


        #endregion

        #region PollInterruption Interruption
        public async Task<Response> AddPollInterruption(string boothMasterId, string stopTime, string ResumeTime, string reason)
        {

            var boothMasterRecord = await _eamsRepository.GetBoothRecord(Convert.ToInt32(boothMasterId));
            if (boothMasterRecord == null)
            {
                var pollInterruptionRecord = await _eamsRepository.GetPollInterruptionData(boothMasterId);

                if (pollInterruptionRecord == null)
                {
                    // check stop time only as it is Fresh record
                    if (stopTime != null && reason != null)
                    {
                        int interruptionreason = Convert.ToInt16(reason);

                        // Get CU,BU from user
                        bool ishmmformat = IsHHmmFormat(stopTime);
                        if (ishmmformat)
                        {
                            DateTime currentTime = DateTime.Now;
                            DateTime stopTimeConvert = DateTime.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);
                            TimeOnly stopTimeConverttime = TimeOnly.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);
                            if (stopTimeConvert <= currentTime)
                            {
                                PollInterruption pollInterruptionData = new PollInterruption()
                                {
                                    StateMasterId = boothMasterRecord.StateMasterId,
                                    DistrictMasterId = boothMasterRecord.DistrictMasterId,
                                    AssemblyMasterId = boothMasterRecord.AssemblyMasterId,
                                    BoothMasterId = boothMasterRecord.BoothMasterId,
                                    StopTime = stopTimeConverttime,
                                    InterruptionType = Convert.ToInt16(reason),
                                    Flag = "Initial",
                                    CreatedAt = BharatDateTime(),
                                    UpdatedAt = BharatDateTime(),
                                    IsPollInterrupted = true,


                                };

                                if ((InterruptionReason)interruptionreason == InterruptionReason.EVMFault)
                                {
                                    //get Cu BU old new
                                    pollInterruptionData.NewCU = "";
                                    pollInterruptionData.NewBU = "";
                                    pollInterruptionData.OldBU = "";
                                    pollInterruptionData.OldCU = "";

                                    _eamsRepository.AddPollInterruption(pollInterruptionData);

                                }
                                else if ((InterruptionReason)interruptionreason == InterruptionReason.LawAndOrder)
                                {
                                    //get Cu BU old new
                                    _eamsRepository.AddPollInterruption(pollInterruptionData);
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.NotFound, Message = "Reason is not Valid !" };
                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Should Not be greater than Current time !" };
                            }
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Please Enter HH:mm Format Only" };
                        }
                        //}
                        //else if ((InterruptionReason)interruptionreason == InterruptionReason.LawAndOrder)
                        //{

                        //}


                    }
                    else
                    {
                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time and Reason is Mandatory to fill!" };
                    }

                }
                else
                {
                    //var boothExists = await _context.BoothMaster.Where(p => p.BoothMasterId == Convert.ToInt32(boothMasterId)).FirstOrDefaultAsync();

                }

            }
            else
            {
                return new Response { Status = RequestStatusEnum.NotFound, Message = "Booth Record Not Found" };
            }
            return null;
        }
        #endregion
        static bool IsHHmmFormat(string timeString)
        {
            DateTime dummyDate; // A dummy date to use for parsing
            return DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummyDate);
        }


    }
}
