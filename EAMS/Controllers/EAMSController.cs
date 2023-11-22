using AutoMapper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAMSController : ControllerBase
    {
        private readonly IEamsService _EAMSService;
        private readonly IMapper _mapper;

        public EAMSController(IEamsService eamsService ,IMapper mapper)
        {
            _EAMSService = eamsService;
            _mapper = mapper;
            
        }

        #region State master
        [HttpGet]
        [Route("StateList")]
        public async Task<IActionResult> StateList()
        {
            var stateList = await _EAMSService.GetState();
            var mappedData = _mapper.Map<List<StateMasterViewModel>>(stateList);

            var data = new
            {
              count= mappedData.Count,
              data= mappedData
            };
            return Ok(data);
        }


        [HttpPut]
        [Route("UpdateStateById")]
        public async Task<IActionResult> UpdateStateById(StateMasterViewModel stateViewModel)
        {
            StateMaster stateMaster = new StateMaster()
            {
                StateMasterId = stateViewModel.StateId,
                StateCode = stateViewModel.StateCode,
                StateName = stateViewModel.StateName
            };
            var state = _EAMSService.UpdateStateById(stateMaster);
            return Ok();
        }

        #endregion

        #region District Master
        [HttpGet]
        [Route("DistrictList")]
        public async Task<IActionResult> DistrictListById(string stateMasterId)
        { 
            var districtList = await _EAMSService.GetDistrictById(stateMasterId);
            var data = new
            {
                count= districtList.Count,
                data= districtList
            };
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateDistrictById")]
        public async Task<IActionResult> UpdateDistrictById(DistrictMasterViewModel districtViewModel)
        {
            DistrictMaster districtMaster = new DistrictMaster()
            {
                StateMasterId = districtViewModel.StateMasterId,
                DistrictMasterId = districtViewModel.DistrictMasterId,
                DistrictCode = districtViewModel.DistrictCode,
                DistrictName = districtViewModel.Name
            };
            var district = _EAMSService.UpdateDistrictById(districtMaster);
            return Ok(district);
        }

        #endregion
             
        #region Assembliy Master

        [HttpGet]
        [Route("GetAssembliesListById")]
        public async Task<IActionResult> AssembliesListById(string stateId, string districtId)
        {
            var assemblyList = await _EAMSService.GetAssemblies(stateId,districtId);  // Corrected to await the asynchronous method
            var data = new
            {
                count=assemblyList.Count,
                data= assemblyList
            };
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateAssembliesById")]
        public async Task<IActionResult> UpdateAssembliesById(AssemblyMasterViewModel assemblyViewModel)
        {
            AssemblyMaster assemblyMaster = new AssemblyMaster()
            {
                StateMasterId = assemblyViewModel.StateMasterId,
                DistrictMasterId = assemblyViewModel.DistrictMasterId,
                AssemblyMasterId = assemblyViewModel.AssemblyMasterId,
                AssemblyCode = assemblyViewModel.AssemblyCode,
                AssemblyName = assemblyViewModel.AssemblyName,
                AssemblyType = assemblyViewModel.AssemblyType,
            };
            var assembly = await _EAMSService.UpdateAssembliesById(assemblyMaster);
            return Ok(assembly);
        }
        #endregion

        #region  SO Master
        [HttpGet]
        [Route("GetSectorOfficersListById")]
        public async Task<IActionResult> SectorOfficersListById(string stateMasterId,string districtMasterId,string assemblyMasterId)
        {
            var soList = await _EAMSService.GetSectorOfficersListById(stateMasterId,districtMasterId,assemblyMasterId);  // Corrected to await the asynchronous method
            var data = new
            {
                count = soList.Count,
                data = soList
            };
            return Ok(data);
        }
        
        
        #endregion

        #region Booth Master

        [HttpGet]
        [Route("GetBoothListById")]
        public async Task<IActionResult> BoothListById(string stateMasterId,string districtMasterId, string assemblyMasterId)
        {
            var boothList = await _EAMSService.GetBoothListById(stateMasterId,districtMasterId,assemblyMasterId);  // Corrected to await the asynchronous method
            var data = new
            {
                count=boothList.Count,
                data= boothList
            };
            return Ok(data);
        }

        #endregion

         
       
    }
}
