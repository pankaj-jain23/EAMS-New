using EAMS_ACore.HelperModels;
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


        #endregion

        #region District Master
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster);
        #endregion

        #region Assembly Master
        Task<List<CombinedMaster>> GetAssemblies(string stateId, string assemblyId);
        Task<AssemblyMaster> UpdateAssembliesById(AssemblyMaster assemblyMaster);
        #endregion

        #region SO Master
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateId);
        #endregion

        #region BoothMaster
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);

        #endregion

    }
}
