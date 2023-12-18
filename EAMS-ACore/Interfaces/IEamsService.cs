using EAMS.Helper;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;

namespace EAMS_ACore.Interfaces
{
    public interface IEamsService
    {
        #region State Master
        Task<List<StateMaster>> GetState();
        Task<Response> UpdateStateById(StateMaster stateMaster);
        Task<Response> AddState(StateMaster stateMaster);

        #endregion

        #region District Master
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<Response> UpdateDistrictById(DistrictMaster districtMaster);
        Task<Response> AddDistrict(DistrictMaster districtMaster);

        #endregion

        #region Assembly Master
        Task<List<CombinedMaster>> GetAssemblies(string stateId, string assemblyId);
        Task<Response> UpdateAssembliesById(AssemblyMaster assemblyMaster);
        Task<Response> AddAssemblies(AssemblyMaster assemblyMaster);
        #endregion

        #region SO Master
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId);
        Task<SectorOfficerProfile> GetSectorOfficerProfile(string soId);

        Task<Response> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<Response> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<List<CombinedMaster>> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId);
        #endregion

        #region BoothMaster
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);

        Task<Response> AddBooth(BoothMaster boothMaster);
        Task<Response> UpdateBooth(BoothMaster boothMaster);
        Task<Response> BoothMapping(List<BoothMaster> boothMaster);

        Task<Response> ReleaseBooth(BoothMaster boothMaster);
        #endregion

        #region EventMaster
        Task<List<EventMaster>> GetEventList();
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
        Task<QueueViewModel> GetVoterInQueue(string boothMasterId);


        Task<Response> AddVoterTurnOut(string boothMasterId, int eventid, string voterValue);

        Task<List<EventWiseBoothStatus>> EventWiseBoothStatus(string soId);
        Task<List<EventActivityCount>> GetEventListDistrictWiseById(string stateId);
        Task<List<EventActivityCount>> GetEventListAssemblyWiseById(string stateId,string districtId);
        Task<List<EventActivityCount>> GetEventListBoothWiseById(string stateId,string districtId,string assemblyId);
        

        #endregion

        #region SendDashBoardCount
        Task<DashBoardRealTimeCount> GetDashBoardCount();
        #endregion

        #region SlotManagement
        Task<Response> AddEventSlot(List<SlotManagementMaster> addEventSlot);
        Task<List<SlotManagementMaster>> GetEventSlotList();
        #endregion
         

    }
}
