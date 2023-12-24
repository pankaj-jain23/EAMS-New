using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
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

        #region UpdateMaster
        public async Task<ServiceResponse> UpdateMasterStatus(UpdateMasterStatus updateMasterStatus)
        {
            var isSucced = await _eamsRepository.UpdateMasterStatus(updateMasterStatus);
            if (isSucced.IsSucceed)
            {
                return new ServiceResponse { 
                IsSucceed= true,
                Message="Status Updated Successfully"
                };
                 
            }
            else
            {
                return new ServiceResponse
                {
                    IsSucceed = false,
                    Message = "Status not Updated! "
                };
            }
        }
        #endregion

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
        public Task<StateMaster> GetStateById(string stateId)
        {
            return _eamsRepository.GetStateById(stateId);
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
        public async Task<DistrictMaster> GetDistrictRecordById(string districtId)
        {
            return await _eamsRepository.GetDistrictRecordById(districtId);
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

        public async Task<AssemblyMaster> GetAssemblyById(string assemblyMasterId)
        {
            return await _eamsRepository.GetAssemblyById(assemblyMasterId);
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

        public async Task<SectorOfficerMasterCustom> GetSOById(string soMasterId)
        {
            var soRecord = await _eamsRepository.GetSOById(soMasterId);
            //var stateRecord = await _eamsRepository.GetStateById(soRecord.StateMasterId.ToString());
            //var districtRecord = await _eamsRepository.GetDistrictRecordById(stateRecord);
            var soCustomRecord = await _eamsRepository.GetAssemblyByCode(soRecord.SoAssemblyCode.ToString());
            SectorOfficerMasterCustom sectorOfficerMasterCustom = new SectorOfficerMasterCustom()
            {
                StateMasterId = soCustomRecord.StateMasterId,
                StateName = soCustomRecord.StateMaster.StateName,
                DistrictMasterId = soCustomRecord.DistrictMasterId,
                DistrictName = soCustomRecord.DistrictMaster.DistrictName,
                DistrictStatus = soCustomRecord.DistrictMaster.DistrictStatus,
                DistrictCode = soCustomRecord.DistrictMaster.DistrictCode,
                AssemblyMasterId = soCustomRecord.AssemblyMasterId,
                AssemblyName = soCustomRecord.AssemblyName,
                AssemblyCode = soCustomRecord.AssemblyCode, 
                SoMasterId=soRecord.SOMasterId,
                SoName=soRecord.SoName,
                SoMobile=soRecord.SoMobile,
                SoOfficeName=soRecord.SoOfficeName,
                SoDesignation=soRecord.SoDesignation,
                IsStatus=soRecord.SoStatus
                    

            };
            return sectorOfficerMasterCustom;
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

        public async Task<BoothMaster> GetBoothById(string boothMasterId)
        {
            return await _eamsRepository.GetBoothById(boothMasterId);
        }

        #endregion

        #region Event Master
        public Task<List<EventMaster>> GetEventList()
        {
            return _eamsRepository.GetEventList();
        }

        public async Task<ServiceResponse> UpdateEventStaus(EventMaster eventMaster)
        {
            var isSucced = await _eamsRepository.UpdateEventStaus(eventMaster);
            if (isSucced.IsSucceed)
            {
                return new ServiceResponse
                {
                    IsSucceed = true,
                    Message = "Status Updated Successfully"
                };

            }
            else
            {
                return new ServiceResponse
                {
                    IsSucceed = false,
                    Message = "Status not Updated! "
                };
            }
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

        public async Task<Response> AddPollInterruption(PollInterruption pollInterruption)
        {

            var boothMasterRecord = await _eamsRepository.GetBoothRecord(Convert.ToInt32(pollInterruption.BoothMasterId));
            if (boothMasterRecord != null)
            {

                var pollInterruptionRecord = await _eamsRepository.GetPollInterruptionData(pollInterruption.BoothMasterId.ToString());

                if (pollInterruptionRecord == null) // if no poll added in table
                {

                    // check stop time or if resume time only as it is Fresh record
                    if (pollInterruption.StopTime != null && pollInterruption.ResumeTime != null)
                    { // check both time in HHM format && comaprison wd each other and from current time
                        bool isStopformat = IsHHmmFormat(pollInterruption.StopTime.ToString()); bool isResumeformat = IsHHmmFormat(pollInterruption.ResumeTime.ToString());
                        if (isStopformat == true && isResumeformat == true)
                        {
                            bool StopTimeisLessEqualToCurrentTime = StopTimeConvertTimeOnly(pollInterruption.StopTime.ToString());
                            bool ResumeTimeisLessEqualToCurrentTime = ResumeTimeConvertTimeOnly(pollInterruption.ResumeTime.ToString());
                            if (StopTimeisLessEqualToCurrentTime)
                            {
                                if (ResumeTimeisLessEqualToCurrentTime)
                                {
                                    bool isResumeGreaterOrEqualToStopTime = CompareStopandResumeTime(pollInterruption.StopTime.ToString(), pollInterruption.ResumeTime.ToString());
                                    if (isResumeGreaterOrEqualToStopTime)
                                    {
                                        PollInterruption pollInterruptionData = new PollInterruption()
                                        {
                                            StateMasterId = boothMasterRecord.StateMasterId,
                                            DistrictMasterId = boothMasterRecord.DistrictMasterId,
                                            AssemblyMasterId = boothMasterRecord.AssemblyMasterId,
                                            BoothMasterId = boothMasterRecord.BoothMasterId,
                                        };
                                        if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.EVMFault)

                                        {
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.NewCU = pollInterruption.NewCU;
                                            pollInterruptionData.NewBU = pollInterruption.NewBU;
                                            pollInterruptionData.OldCU = pollInterruption.OldCU;
                                            pollInterruptionData.OldBU = pollInterruption.OldBU;
                                            pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Both.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = false;

                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;

                                        }
                                        else if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.LawAndOrder)
                                        {

                                            pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Both.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = false;
                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;
                                        }
                                        else
                                        {
                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Reason is not Valid" };
                                        }



                                    }
                                    else
                                    {
                                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Resume Time Cannot be less than Stop Time" };

                                    }

                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Resume Time Cannot be greater than Current Time" };

                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Cannot be greater than Current Time" };

                            }


                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Time formats should be in 24Hr. format" };

                        }
                    }
                    else if (pollInterruption.StopTime != null && pollInterruption.ResumeTime == null)
                    {
                        // user is entering only stopTime, so check hhm format && comprae from current time
                        bool isStopformat = IsHHmmFormat(pollInterruption.StopTime.ToString());

                        if (isStopformat == true)
                        {
                            bool StopTimeisLessEqualToCurrentTime = StopTimeConvertTimeOnly(pollInterruption.StopTime.ToString());
                            if (StopTimeisLessEqualToCurrentTime)
                            {
                                PollInterruption pollInterruptionData = new PollInterruption()
                                {
                                    StateMasterId = boothMasterRecord.StateMasterId,
                                    DistrictMasterId = boothMasterRecord.DistrictMasterId,
                                    AssemblyMasterId = boothMasterRecord.AssemblyMasterId,
                                    BoothMasterId = boothMasterRecord.BoothMasterId,
                                };
                                if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.EVMFault)
                                {
                                    pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                    pollInterruptionData.NewCU = pollInterruption.NewCU;
                                    pollInterruptionData.NewBU = pollInterruption.NewBU;
                                    pollInterruptionData.OldCU = pollInterruption.OldCU;
                                    pollInterruptionData.OldBU = pollInterruption.OldBU;
                                    pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);

                                    pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                    pollInterruptionData.Flag = InterruptionCategory.Stop.ToString();
                                    pollInterruptionData.CreatedAt = BharatDateTime();
                                    pollInterruptionData.UpdatedAt = BharatDateTime();
                                    pollInterruptionData.IsPollInterrupted = true;

                                    var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                    return result.Result;

                                }
                                else if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.LawAndOrder)
                                {

                                    pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                    pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                    pollInterruptionData.Flag = InterruptionCategory.Stop.ToString();
                                    pollInterruptionData.CreatedAt = BharatDateTime();
                                    pollInterruptionData.UpdatedAt = BharatDateTime();
                                    pollInterruptionData.IsPollInterrupted = true;
                                    var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                    return result.Result;
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Reason is not Valid" };
                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Cannot be greater than Current Time" };
                            }
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time format should be in 24Hr. format" };

                        }
                    }
                    else if (pollInterruption.StopTime == null && pollInterruption.ResumeTime != null)
                    {
                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time cannot be Empty!" };

                    }

                }
                // Poll interrupted data Already in database like resolved or pending        
                else
                {
                    if (pollInterruptionRecord.StopTime != null && pollInterruptionRecord.ResumeTime != null)
                    {
                        if (pollInterruption.StopTime != null && pollInterruption.ResumeTime != null)
                        { // check both time in HHM format && comaprison wd each other and from current time
                            bool isStopformat = IsHHmmFormat(pollInterruption.StopTime.ToString()); bool isResumeformat = IsHHmmFormat(pollInterruption.ResumeTime.ToString());
                            if (isStopformat == true && isResumeformat == true)
                            {
                                // check last Resume time with pollInterruption.StopTime, it should be greater than stop
                                bool IsNewStopGreaterLastResumeTime = CheckLastResumeTime2(pollInterruptionRecord.ResumeTime, pollInterruption.StopTime.ToString());
                                if (IsNewStopGreaterLastResumeTime == true)
                                {


                                    bool StopTimeisLessEqualToCurrentTime = StopTimeConvertTimeOnly(pollInterruption.StopTime.ToString());
                                    bool ResumeTimeisLessEqualToCurrentTime = ResumeTimeConvertTimeOnly(pollInterruption.ResumeTime.ToString());
                                    if (StopTimeisLessEqualToCurrentTime)
                                    {
                                        if (ResumeTimeisLessEqualToCurrentTime)
                                        {
                                            bool isResumeGreaterOrEqualToStopTime = CompareStopandResumeTime(pollInterruption.StopTime.ToString(), pollInterruption.ResumeTime.ToString());
                                            if (isResumeGreaterOrEqualToStopTime)
                                            {
                                                PollInterruption pollInterruptionData = new PollInterruption()
                                                {
                                                    StateMasterId = pollInterruptionRecord.StateMasterId,
                                                    DistrictMasterId = pollInterruptionRecord.DistrictMasterId,
                                                    AssemblyMasterId = pollInterruptionRecord.AssemblyMasterId,
                                                    BoothMasterId = pollInterruptionRecord.BoothMasterId,
                                                };
                                                if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.EVMFault)

                                                {
                                                    pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                                    pollInterruptionData.NewCU = pollInterruption.NewCU;
                                                    pollInterruptionData.NewBU = pollInterruption.NewBU;
                                                    pollInterruptionData.OldCU = pollInterruption.OldCU;
                                                    pollInterruptionData.OldBU = pollInterruption.OldBU;
                                                    pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                                    pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                                    pollInterruptionData.Flag = InterruptionCategory.Both.ToString();
                                                    pollInterruptionData.CreatedAt = BharatDateTime();
                                                    pollInterruptionData.UpdatedAt = BharatDateTime();
                                                    pollInterruptionData.IsPollInterrupted = false;

                                                    var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                                    return result.Result;

                                                }
                                                else if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.LawAndOrder)
                                                {

                                                    pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                                    pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                                    pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                                    pollInterruptionData.Flag = InterruptionCategory.Both.ToString();
                                                    pollInterruptionData.CreatedAt = BharatDateTime();
                                                    pollInterruptionData.UpdatedAt = BharatDateTime();
                                                    pollInterruptionData.IsPollInterrupted = false;
                                                    var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                                    return result.Result;
                                                }
                                                else
                                                {
                                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Reason is not Valid" };
                                                }



                                            }
                                            else
                                            {
                                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Resume Time Cannot be less than Stop Time" };

                                            }

                                        }
                                        else
                                        {
                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Resume Time Cannot be greater than Current Time" };

                                        }
                                    }
                                    else
                                    {
                                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Cannot be greater than Current Time" };

                                    }

                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time should be greater than from Last Resume Time Entered" };
                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Time formats should be in 24Hr. format" };

                            }
                        }
                        else if (pollInterruption.StopTime != null && pollInterruption.ResumeTime == null)
                        {
                            // user is entering only stopTime, so check hhm format && comprae from current time
                            bool isStopformat = IsHHmmFormat(pollInterruption.StopTime.ToString());

                            if (isStopformat == true)
                            {
                                bool StopTimeisLessEqualToCurrentTime = StopTimeConvertTimeOnly(pollInterruption.StopTime.ToString());
                                if (StopTimeisLessEqualToCurrentTime)
                                {

                                    // check last entered resume , newstoptime must be greater than equal to lastrsume
                                    bool IsStopGreaterThanLastResumeTime = CheckLastResumeTime2(pollInterruptionRecord.ResumeTime, pollInterruption.StopTime.ToString());
                                    if (IsStopGreaterThanLastResumeTime == true)
                                    {
                                        PollInterruption pollInterruptionData = new PollInterruption()
                                        {
                                            StateMasterId = boothMasterRecord.StateMasterId,
                                            DistrictMasterId = boothMasterRecord.DistrictMasterId,
                                            AssemblyMasterId = boothMasterRecord.AssemblyMasterId,
                                            BoothMasterId = boothMasterRecord.BoothMasterId,
                                        };
                                        if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.EVMFault)

                                        {
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.NewCU = pollInterruption.NewCU;
                                            pollInterruptionData.NewBU = pollInterruption.NewBU;
                                            pollInterruptionData.OldCU = pollInterruption.OldCU;
                                            pollInterruptionData.OldBU = pollInterruption.OldBU;
                                            pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);

                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Stop.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = true;

                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;

                                        }
                                        else if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.LawAndOrder)

                                        {

                                            pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Stop.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = true;
                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;
                                        }
                                        else
                                        {
                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Reason is not Valid" };
                                        }
                                    }
                                    else
                                    {
                                        return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Should be Greater than Last Entered Resume Time" };
                                    }
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time Cannot be greater than Current Time" };
                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time format should be in 24Hr. format" };

                            }
                        }
                        else if (pollInterruption.StopTime == null && pollInterruption.ResumeTime != null)
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Stop Time cannot be Empty!" };

                        }



                    }

                    //case cleared
                    else if (pollInterruptionRecord.StopTime != null && pollInterruptionRecord.ResumeTime == null)
                    {
                        //need to enter Resume Time
                        if (pollInterruption.ResumeTime.ToString() != null)
                        {
                            bool isResumeformat = IsHHmmFormat(pollInterruption.ResumeTime.ToString());

                            if (isResumeformat == true)
                            {
                                bool ResumeTimeisLessEqualToCurrentTime = ResumeTimeConvertTimeOnly(pollInterruption.ResumeTime.ToString());
                                if (ResumeTimeisLessEqualToCurrentTime == true)
                                {

                                    bool IsNewResumeTimeGreaterLastStopTime = CheckLastStopTime(pollInterruptionRecord.StopTime, pollInterruption.ResumeTime.ToString());
                                    if (IsNewResumeTimeGreaterLastStopTime == true)
                                    {
                                        PollInterruption pollInterruptionData = new PollInterruption()
                                        {
                                            StateMasterId = pollInterruptionRecord.StateMasterId,
                                            DistrictMasterId = pollInterruptionRecord.DistrictMasterId,
                                            AssemblyMasterId = pollInterruptionRecord.AssemblyMasterId,
                                            BoothMasterId = pollInterruptionRecord.BoothMasterId,
                                        };
                                        if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.EVMFault)

                                        {
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.NewCU = pollInterruption.NewCU;
                                            pollInterruptionData.NewBU = pollInterruption.NewBU;
                                            pollInterruptionData.OldCU = pollInterruption.OldCU;
                                            pollInterruptionData.OldBU = pollInterruption.OldBU;
                                            pollInterruptionData.StopTime = TimeOnly.ParseExact(pollInterruption.StopTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.InterruptionType = pollInterruptionRecord.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Resume.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = false;

                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;

                                        }
                                        else if ((InterruptionReason)pollInterruption.InterruptionType == InterruptionReason.LawAndOrder)

                                        {

                                            pollInterruptionData.StopTime = pollInterruptionRecord.StopTime;
                                            pollInterruptionData.ResumeTime = TimeOnly.ParseExact(pollInterruption.ResumeTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                                            pollInterruptionData.InterruptionType = pollInterruption.InterruptionType;
                                            pollInterruptionData.Flag = InterruptionCategory.Resume.ToString();
                                            pollInterruptionData.CreatedAt = BharatDateTime();
                                            pollInterruptionData.UpdatedAt = BharatDateTime();
                                            pollInterruptionData.IsPollInterrupted = false;
                                            var result = _eamsRepository.AddPollInterruption(pollInterruptionData);
                                            return result.Result;
                                        }
                                        else
                                        {
                                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Reason is not Valid" };
                                        }

                                    }
                                    else
                                    {
                                        return new Response { Status = RequestStatusEnum.NotFound, Message = "Resume Time must be greater than Last Entered Stop Time." };

                                    }
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.NotFound, Message = "Resume Time Cannot be greater than Current Time." };

                                }
                            }
                            else
                            {
                                return new Response { Status = RequestStatusEnum.NotFound, Message = "Resume Time must be in 24Hr Format." };
                            }
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.NotFound, Message = "Please Enter Resume Time in 24Hr Format." };
                        }

                    }


                }

            }
            else
            {
                return new Response { Status = RequestStatusEnum.NotFound, Message = "Booth Record Not Found" };
            }
            return null;
        }

        //public async Task<PollInterruption> GetPollInterruption(string boothMasterId)
        //{
        //    var res = await _eamsRepository.GetPollInterruptionData(boothMasterId);
        //    var boothExists = await _eamsRepository.GetBoothRecord(Convert.ToInt32(boothMasterId));
        //    if (res.StopTime != null && res.ResumeTime != null)
        //    {
        //        PollInterruption pollInterruptionData = new PollInterruption()
        //        {
        //            StateMasterId = res.StateMasterId,
        //            DistrictMasterId = res.DistrictMasterId,
        //            AssemblyMasterId = res.AssemblyMasterId,
        //            BoothMasterId = res.BoothMasterId,
        //            StopTime = res.StopTime,
        //            ResumeTime = res.ResumeTime,
        //            PollInterruptionId = res.PollInterruptionId,
        //            InterruptionType = res.InterruptionType,
        //            Flag = "New",
        //            UpdatedAt = res.UpdatedAt,
        //            IsPollInterrupted = false,


        //        };
        //        return pollInterruptionData;
        //    }
        //    else if (res.StopTime != null && res.ResumeTime == null)
        //    {
        //        PollInterruption pollInterruptionData = new PollInterruption()
        //        {
        //            StateMasterId = res.StateMasterId,
        //            DistrictMasterId = res.DistrictMasterId,
        //            AssemblyMasterId = res.AssemblyMasterId,
        //            BoothMasterId = res.BoothMasterId,
        //            StopTime = res.StopTime,
        //            ResumeTime = res.ResumeTime,
        //            PollInterruptionId = res.PollInterruptionId,
        //            InterruptionType = res.InterruptionType,
        //            Flag = "Resume",
        //            UpdatedAt = res.UpdatedAt,
        //            IsPollInterrupted = true,


        //        };
        //        return pollInterruptionData;
        //    }
        //    else if (res == null)
        //    {
        //        PollInterruption pollInterruptionData = new PollInterruption()
        //        {
        //            StateMasterId = res.StateMasterId,
        //            DistrictMasterId = res.DistrictMasterId,
        //            AssemblyMasterId = res.AssemblyMasterId,
        //            BoothMasterId = res.BoothMasterId,
        //            StopTime = res.StopTime,
        //            ResumeTime = res.ResumeTime,
        //            PollInterruptionId = res.PollInterruptionId,
        //            InterruptionType = res.InterruptionType,
        //            Flag = "Initial",
        //            IsPollInterrupted = false,


        //        };
        //        return pollInterruptionData;
        //    }
        //    else
        //    {
        //        PollInterruption pollInterruptionData = new PollInterruption()
        //        {
        //            StateMasterId = boothExists.StateMasterId,
        //            DistrictMasterId = boothExists.DistrictMasterId,
        //            AssemblyMasterId = boothExists.AssemblyMasterId,
        //            BoothMasterId = boothExists.BoothMasterId,
        //            StopTime = null,
        //            ResumeTime = null,
        //            Flag = "Initial",
        //            IsPollInterrupted = false,


        //        };
        //        return pollInterruptionData;

        //    }
        //}


        public Task<PollInterruption> GetPollInterruptionbyId(string boothMasterId)
        {
            return _eamsRepository.GetPollInterruptionData(boothMasterId);
        }
        #endregion
        static bool IsHHmmFormat(string timeString)
        {
            DateTime dummyDate; // A dummy date to use for parsing
            return DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummyDate);
        }

        static bool StopTimeConvertTimeOnly(string stopTime)
        {
            DateTime currentTime = DateTime.Now;
            DateTime stopTimeConvert = DateTime.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);
            TimeOnly stopTimeConverttime = TimeOnly.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);
            if (stopTimeConvert <= currentTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool ResumeTimeConvertTimeOnly(string resumeTime)
        {
            DateTime currentTime = DateTime.Now;
            DateTime resumeTimeConvert = DateTime.ParseExact(resumeTime, "HH:mm", CultureInfo.InvariantCulture);
            TimeOnly resumeTimeConvertTime = TimeOnly.ParseExact(resumeTime, "HH:mm", CultureInfo.InvariantCulture);
            if (resumeTimeConvert <= currentTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool CompareStopandResumeTime(string stopTime, string resumeTime)
        {
            DateTime currentTime = DateTime.Now;
            DateTime stopTimeConvert = DateTime.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);
            //TimeOnly stopTimeConvertTime = TimeOnly.ParseExact(stopTime, "HH:mm", CultureInfo.InvariantCulture);

            DateTime resumeTimeConvert = DateTime.ParseExact(resumeTime, "HH:mm", CultureInfo.InvariantCulture);
            // TimeOnly resumeTimeConvertTime = TimeOnly.ParseExact(resumeTime, "HH:mm", CultureInfo.InvariantCulture);
            if (resumeTimeConvert >= stopTimeConvert)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool CheckLastResumeTime(TimeOnly? InterruptionRecordResumeTime, string newStopTime)
        {


            TimeOnly newStopTimeConverttime = TimeOnly.ParseExact(newStopTime, "HH:mm", CultureInfo.InvariantCulture);
            if (InterruptionRecordResumeTime >= newStopTimeConverttime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool CheckLastResumeTime2(TimeOnly? InterruptionRecordResumeTime, string newStopTime)
        {


            TimeOnly newStopTimeConverttime = TimeOnly.ParseExact(newStopTime, "HH:mm", CultureInfo.InvariantCulture);
            if (newStopTimeConverttime >= InterruptionRecordResumeTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool CheckLastStopTime(TimeOnly? InterruptionRecordStopTime, string newResumeTime)
        {


            TimeOnly newResumeTimeConverttime = TimeOnly.ParseExact(newResumeTime, "HH:mm", CultureInfo.InvariantCulture);
            if (newResumeTimeConverttime >= InterruptionRecordStopTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetInterruptionReason(string reason)
        {
            int interruptionreason = Convert.ToInt16(reason);
            string reasonStatus = "";
            if ((InterruptionReason)interruptionreason == InterruptionReason.EVMFault)
            {
                reasonStatus = InterruptionReason.EVMFault.ToString();



            }
            else if ((InterruptionReason)interruptionreason == InterruptionReason.LawAndOrder)
            {
                reasonStatus = InterruptionReason.LawAndOrder.ToString();
            }
            else
            {
                reasonStatus = "";
            }
            return reasonStatus;
        }

      
    }
}
