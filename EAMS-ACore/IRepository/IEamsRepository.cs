using EAMS_ACore.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IRepository
{
    public interface IEamsRepository
    {
        Task<List<CombinedMaster>> GetDistrictById(string stateMasterId);
        Task<List<StateMaster>> GetState();

        Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId);
        Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId);

        Task<List<CombinedMaster>> GetBoothListById(string stateMasterId,string districtMasterId, string assemblyMasterId);

        
        Task<StateMaster> UpdateStateById(StateMaster stateMaster);

        //UpadteDistrict
        Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster);
    }
}
