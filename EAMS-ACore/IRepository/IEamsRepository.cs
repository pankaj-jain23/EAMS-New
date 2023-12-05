using EAMS.Helper;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IRepository
{
    public interface IEamsRepository
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
        Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId);
        Task<Response> UpdateAssembliesById(AssemblyMaster assemblyMaster);

        Task<Response> AddAssemblies(AssemblyMaster assemblyMaster);
        #endregion

        #region SO Master
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId);
        Task<Response> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<Response> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<List<CombinedMaster>> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId);
        #endregion

        #region Booth Master
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);
        Task<Response> AddBooth(BoothMaster boothMaster);
        Task<Response> UpdateBooth(BoothMaster boothMaster);
        Task<Response> BoothMapping(List<BoothMaster> boothMaster);
        Task<Response> ReleaseBooth(BoothMaster boothMaster);
        #endregion

        #region Event Master
        Task<List<EventMaster>> GetEventList();
        Task<Response> UpdateEventById(EventMaster eventMaster);
        #endregion

        #region PCMaster
        Task<List<ParliamentConstituencyMaster>> GetPCList();
        #endregion


        #region Event Activity
        Task<Response> EventActivity(ElectionInfoMaster electionInfoMaster);
        Task<ElectionInfoMaster> EventUpdationStatus(ElectionInfoMaster electionInfoMaster);

        Task<List<EventWiseBoothStatus>> EventWiseBoothStatus(string soId);
        // eventid = 2
        // prev= 1, nxt=3

        // getprev== true
        //{// nxt== false} 




        #endregion
    }
}
