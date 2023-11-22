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

        #region State Master

        public Task<List<StateMaster>> GetState()
        {
            return _eamsRepository.GetState();
        }

        public Task<StateMaster> UpdateStateById(StateMaster stateMaster)
        {
            return _eamsRepository.UpdateStateById(stateMaster);
        }

        #endregion

        #region District Master

        public Task<List<CombinedMaster>> GetDistrictById(string stateMasterId)
        {
         return _eamsRepository.GetDistrictById(stateMasterId);
        }


        public Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster)
        {
            return _eamsRepository.UpdateDistrictById(districtMaster);

        }
        #endregion

      

        #region Assembly  Master
        public Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId)
        {
            return _eamsRepository.GetAssemblies(stateId, districtId);
        }

        public Task<AssemblyMaster> UpdateAssembliesById(AssemblyMaster assemblyMaster) 
        {
            return _eamsRepository.UpdateAssembliesById(assemblyMaster);
        }
        #endregion

        #region  SO Master
        public Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return _eamsRepository.GetSectorOfficersListById(stateMasterId,districtMasterId,assemblyMasterId);
        }
        #endregion

        #region Booth Master
        
        public Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return _eamsRepository.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);
        }
        public string AddBooth(BoothMaster boothMaster)
        {
            return _eamsRepository.AddBooth(boothMaster);

        }

        #endregion


    }
}
