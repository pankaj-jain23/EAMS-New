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
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<List<StateMaster>> GetState();

        Task<List<CombinedMaster>> GetAssemblies(string stateId, string assemblyId);

        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateId);
        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId);

        Task<StateMaster> UpdateStateById(StateMaster stateMaster);

        //UpdateDistrictById
        Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster);
    }
}
