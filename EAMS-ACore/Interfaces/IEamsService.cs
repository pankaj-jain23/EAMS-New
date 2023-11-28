using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.Interfaces
{
    public interface IEamsService
    {
        #region State Master
        Task<List<StateMaster>> GetState();
        Task<StateMaster> UpdateStateById(StateMaster stateMaster);
        string AddState(StateMaster stateMaster);

        #endregion

        #region District Master
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster);
        string AddDistrict(DistrictMaster districtMaster);

        #endregion

        #region Assembly Master
        Task<List<CombinedMaster>> GetAssemblies(string stateId, string assemblyId);
        Task<AssemblyMaster> UpdateAssembliesById(AssemblyMaster assemblyMaster);
        string AddAssemblies(AssemblyMaster assemblyMaster);
        #endregion

        #region SO Master
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId,string districtMasterId, string assemblyMasterId);
        Task<string> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        Task<string> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster);
        #endregion

        #region BoothMaster
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);

        string AddBooth(BoothMaster boothMaster);
        Task<string> UpdateBooth(BoothMaster boothMaster);
        Task<string> BoothMapping(List<BoothMaster> boothMaster);


        #endregion

        #region EventMaster
        Task<List<EventMaster>> GetEventList();
        Task<EventMaster> UpdateEventById(EventMaster eventMaster);


        #endregion

        #region PCMaster

        Task<List<ParliamentConstituencyMaster>> GetPCList();
        #endregion

    }
}
