﻿using EAMS.Helper;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;

namespace EAMS_ACore.Interfaces
{
    public interface IEamsService
    {
        #region UpdateMasterStatus
        Task<ServiceResponse> UpdateMasterStatus(UpdateMasterStatus updateMasterStatus);
        #endregion

        #region State Master
        Task<List<StateMaster>> GetState();
        Task<Response> UpdateStateById(StateMaster stateMaster);
        Task<Response> AddState(StateMaster stateMaster);
        Task<StateMaster> GetStateById(string Id);

        #endregion

        #region District Master
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<Response> UpdateDistrictById(DistrictMaster districtMaster);
        Task<Response> AddDistrict(DistrictMaster districtMaster);
        Task<DistrictMaster> GetDistrictRecordById(string districtId);
        #endregion

        #region Assembly Master
        Task<List<CombinedMaster>> GetAssemblies(string stateId, string assemblyId);
        Task<Response> UpdateAssembliesById(AssemblyMaster assemblyMaster);
        Task<Response> AddAssemblies(AssemblyMaster assemblyMaster);
        Task<AssemblyMaster> GetAssemblyById(string assemblyId);
        #endregion

        #region SO Master
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId);
        Task<SectorOfficerProfile> GetSectorOfficerProfile(string soId);

        Task<Response> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<Response> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<List<CombinedMaster>> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId);
        Task<SectorOfficerMasterCustom> GetSOById(string soId);
        #endregion

        #region BoothMaster
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);

        Task<Response> AddBooth(BoothMaster boothMaster);
        Task<Response> UpdateBooth(BoothMaster boothMaster);
        Task<Response> BoothMapping(List<BoothMaster> boothMaster);

        Task<Response> ReleaseBooth(BoothMaster boothMaster);

        Task<BoothMaster> GetBoothById(string boothMasterId);
        #endregion

        #region EventMaster
        Task<List<EventMaster>> GetEventList();
        Task<ServiceResponse> UpdateEventStaus(EventMaster eventMaster);
        Task<Response> UpdateEventById(EventMaster eventMaster);
        Task<List<EventWiseBooth>> GetBoothListByEventId(string eventId, string soId);
        Task<List<EventWiseBooth>> GetBoothStatusforARO(string assemblyMasterId, string boothMasterId);

        #endregion

        #region PCMaster

        Task<List<ParliamentConstituencyMaster>> GetPCList();
        #endregion

        #region Event Activity
        Task<Response> EventActivity(ElectionInfoMaster electionInfoMaster);
        
        Task<VoterTurnOutPolledDetailViewModel> GetLastUpdatedPollDetail(string boothMasterId, int eventid);
        Task<Queue> GetVoterInQueue(string boothMasterId);
        Task<FinalViewModel> GetFinalVotes(string boothMasterId);

        Task<Response> AddVoterTurnOut(string boothMasterId, int eventid, string voterValue);

        Task<List<EventWiseBoothStatus>> EventWiseBoothStatus(string soId);
        Task<List<EventActivityCount>> GetEventListDistrictWiseById(string stateId);
        Task<List<AssemblyEventActivityCount>> GetEventListAssemblyWiseById(string stateId,string districtId);
        Task<List<EventActivityBoothWise>> GetEventListBoothWiseById(string stateId,string districtId,string assemblyId);
        

        #endregion

        #region SendDashBoardCount
        Task<DashBoardRealTimeCount> GetDashBoardCount();
        #endregion

        #region SlotManagement
        Task<Response> AddEventSlot(List<SlotManagementMaster> addEventSlot);
        Task<List<SlotManagementMaster>> GetEventSlotList();
        #endregion

        Task<List<UserList>> GetUserList(string soName, string type);

        #region PollInterruption Interruption
        Task<Response> AddPollInterruption(PollInterruption Pollinterruptionl);
        
        Task<PollInterruption> GetPollInterruptionbyId(string interruptionMasterId);
        Task<List<PollInterruptionDashboard>> GetPollInterruptionDashboard(string stateId);

        #endregion

    }
}
