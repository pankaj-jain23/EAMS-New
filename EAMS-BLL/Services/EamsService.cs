using EAMS.Helper;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
using EAMS_DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Response>  AddAssemblies(AssemblyMaster assemblyMaster)
        {
            return await _eamsRepository.AddAssemblies(assemblyMaster);
        }
        #endregion

        #region  SO Master
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return await _eamsRepository.GetSectorOfficersListById(stateMasterId, districtMasterId, assemblyMasterId);
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
                            if (electionInfoRecord.IsPartyDispatched==false) {
                                return await _eamsRepository.EventActivity(electionInfoMaster);
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
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Has Already Reached." };
                        }
                    case 2:
                        if (electionInfoRecord.IsPartyDispatched == true)
                        {
                            if (electionInfoRecord.IsPartyReached == false || electionInfoRecord.IsPartyReached == null)
                            {
                                if (electionInfoRecord.IsSetupOfPolling == false || electionInfoRecord.IsSetupOfPolling == null)
                                {
                                    electionInfoRecord.IsPartyReached = true;
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
                                
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Reached Status Already Yes." };
                            }
                        }
                        else
                        {
                         
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Dispatched Yet." };
                        }
                    case 3:
                        if (electionInfoRecord.IsPartyReached == true)
                        {

                            if (electionInfoRecord.IsSetupOfPolling == false || electionInfoRecord.IsSetupOfPolling == null)
                            {
                                if (electionInfoRecord.IsMockPollDone == false || electionInfoRecord.IsMockPollDone == null)
                                {
                                    electionInfoRecord.IsSetupOfPolling = true;
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
                              // already status Yes
                                return new Response { Status = RequestStatusEnum.BadRequest, Message = "SetUp Polling Status Already yes." };

                            }

                        }
                        else
                        {
                            return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Reached Yet." };

                        }

                    default:
                        // Handle the case when EventMasterId doesn't match any known case
                        return null;
                        //return new Response { Status = RequestStatusEnum.BadRequest, Message = "Party Not Reached Yet." };
                }
            }
            else
            {
                return await _eamsRepository.EventActivity(electionInfoMaster);
            }

        }
        #endregion

    }
}
