using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
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
        public Task<List<CombinedMaster>> GetDistrictById(string stateMasterId)
        {
         return _eamsRepository.GetDistrictById(stateMasterId);
        }

        public Task<List<StateMaster>> GetState()
        {
            return _eamsRepository.GetState();
        }
        public Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId)
        {
            return _eamsRepository.GetAssemblies(stateId, districtId);
        }
        public Task<List<CombinedMaster>> GetSectorOfficersListById(string stateId)
        {
            return _eamsRepository.GetSectorOfficersListById(stateId);
        }


        public Task<StateMaster> UpdateStateById(StateMaster stateMaster)
        {
             return _eamsRepository.UpdateStateById(stateMaster);
        }


        public Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return _eamsRepository.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);
        }
        public Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster)
        {
            return _eamsRepository.UpdateDistrictById(districtMaster);

        }
    }
}
