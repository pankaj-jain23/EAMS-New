using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.IRepository;
using EAMS_ACore.Models;
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
        public string AddState(StateMaster stateMaster)
        {
            return _eamsRepository.AddState(stateMaster);

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

        public string AddDistrict(DistrictMaster districtMaster)
        {
            return _eamsRepository.AddDistrict(districtMaster);
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

        public string AddAssemblies(AssemblyMaster assemblyMaster)
        {
            return _eamsRepository.AddAssemblies(assemblyMaster);
        }
        #endregion

        #region  SO Master
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return await _eamsRepository.GetSectorOfficersListById(stateMasterId,districtMasterId,assemblyMasterId);
        }
        public async Task<string> AddSectorOfficer(SectorOfficerMaster sectorOfficerMaster)
        {
            return await _eamsRepository.AddSectorOfficer(sectorOfficerMaster);
        }

        public async Task<string> UpdateSectorOfficer(SectorOfficerMaster sectorOfficerMaster)
        {
            return await _eamsRepository.UpdateSectorOfficer(sectorOfficerMaster);
        }
        #endregion

        #region Booth Master

        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            return  await _eamsRepository.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);
        }
        public string AddBooth(BoothMaster boothMaster)
        {
            return _eamsRepository.AddBooth(boothMaster);

        }

        public async Task<string> UpdateBooth(BoothMaster boothMaster)
        {
            return await _eamsRepository.UpdateBooth(boothMaster);
        }
        public async Task<string> BoothMapping(List<BoothMaster> boothMaster)
        {
            return await _eamsRepository.BoothMapping(boothMaster);
        }

        #endregion

        #region Event Master
        public Task<List<EventMaster>> GetEventList() 
        {
            return _eamsRepository.GetEventList();
        }

 
        public Task<EventMaster> UpdateEventById(EventMaster eventMaster)
        { 
            return _eamsRepository.UpdateEventById(eventMaster);
        }
        #endregion

        #region PCMaster
        public Task<List<ParliamentConstituencyMaster>> GetPCList()
        {
            return _eamsRepository.GetPCList();
        }
        #endregion

    }
}
