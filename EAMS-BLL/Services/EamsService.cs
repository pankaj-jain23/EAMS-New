using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;

namespace EAMS_BLL.Services
{
    public class EamsService : IEamsService
    {
        private readonly IEamsRepository _eamsRepository;
        public EamsService(IEamsRepository eamsRepository)
        {
            _eamsRepository = eamsRepository;
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
                                return await _eamsRepository.EventActivity(electionInfoMaster);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, MockPoll Already yes." };
                            }



                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Reached Yet." };

                        }
                    case 4:
                        if (electionInfoRecord.IsSetupOfPolling == true) // mockpoll event 4th event
                        {

                            if (electionInfoRecord.IsPollStarted == false || electionInfoRecord.IsPollStarted == null)
                            {
                                electionInfoRecord.IsMockPollDone = electionInfoMaster.IsMockPollDone;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoMaster);
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

                    case 5:
                        if (electionInfoRecord.IsMockPollDone == true) // poll started 5th event
                        {
                            //change final voting to slot check  if it has values then it is freezed otherwise poll started can be done
                            if (electionInfoRecord.FinalTVote == 0 || electionInfoRecord.FinalTVote == null)
                            {
                                electionInfoRecord.IsPollStarted = electionInfoMaster.IsPollStarted;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoMaster);
                            }
                            else
                            {

                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, Voter Turn Out Status is Already Yes." };
                            }



                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Mock Poll Done is not Done Yet." };

                        }

                    case 7:
                        //queue

                        //change final voting to slot check  if it has values then it is freezed otherwise poll started can be done
                        if (electionInfoMaster.VoterInQueue != null && electionInfoMaster.VoterInQueue > 0)
                        {
                            electionInfoRecord.VoterInQueue = electionInfoMaster.VoterInQueue;
                            electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                            return await _eamsRepository.EventActivity(electionInfoMaster);
                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Queue value cannot be Null." };
                        }





                    case 8:
                        // Final Votes
                        if (electionInfoRecord.VoterInQueue > 0) //check queue
                        {

                            if (electionInfoRecord.IsPollEnded == false || electionInfoRecord.IsPollEnded == null)
                            {
                                //change final voting to slot check  if it has values then it is freezed otherwise poll started can be done
                                if (electionInfoMaster.FinalTVote != null && electionInfoMaster.FinalTVote > 0)
                                {
                                    electionInfoRecord.FinalTVote = electionInfoMaster.FinalTVote;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoMaster);
                                }
                                else
                                {
                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Final Votes Cannot be Null." };
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
                                electionInfoRecord.IsPollEnded = true;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoMaster);
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
                                    electionInfoRecord.IsMCESwitchOff = true;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoMaster);
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
                                    electionInfoRecord.IsPartyDeparted = true;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoMaster);
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
                                    electionInfoRecord.IsPartyReachedCollectionCenter = true;
                                    electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                    return await _eamsRepository.EventActivity(electionInfoMaster);
                                }
                                else
                                {

                                    return new Response { Status = RequestStatusEnum.BadRequest, Message = "Can't Change Status, EVM Deposited." };
                                }

                            }
                            else
                            {
                                // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Reached Already Yes." };

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
                                electionInfoRecord.IsEVMDeposited = true;
                                electionInfoRecord.EventMasterId = electionInfoMaster.EventMasterId;
                                return await _eamsRepository.EventActivity(electionInfoMaster);
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


        #endregion

        #region SendDashBoardCount 
        public async Task<DashBoardRealTimeCount> GetDashBoardCount()
        {
            return await _eamsRepository.GetDashBoardCount();
        }
        #endregion
    }
}
