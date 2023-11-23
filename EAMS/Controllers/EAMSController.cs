using AutoMapper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
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

        [HttpPost]
        [Route("AddState")]
        public async Task<IActionResult> AddState(StateMasterViewModel stateMasterViewModel)
        {
            var insertstate = _mapper.Map<StateMasterViewModel, StateMaster>(stateMasterViewModel);
            var result = _EAMSService.AddState(insertstate);


            return Ok(result);
        }


        #endregion

        #region District Master
        [HttpGet]
        [Route("DistrictList")]
        public async Task<IActionResult> DistrictListById(string stateMasterId)
        { 
            var districtList = await _EAMSService.GetDistrictById(stateMasterId);
            var mappedData = _mapper.Map<List<DistrictMasterViewModel>>(districtList);

            var data = new
            {
                count= mappedData.Count,
                data= mappedData
            };
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateDistrictById")]
        public async Task<IActionResult> UpdateDistrictById(DistrictMasterViewModel districtViewModel)
        {
            var mappedData = _mapper.Map<DistrictMasterViewModel,DistrictMaster>(districtViewModel);           
            var district = _EAMSService.UpdateDistrictById(mappedData);
            return Ok(district);
        }
        [HttpPost]
        [Route("AddDistrict")]
        public async Task<IActionResult> AddDistrict(DistrictMasterViewModel districtViewModel)
        {
            var mappedData = _mapper.Map<DistrictMasterViewModel, DistrictMaster>(districtViewModel);
            var add = _EAMSService.AddDistrict(mappedData);
            return Ok(add);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateMasterId"></param>
        /// <param name="districtMasterId"></param>
        /// <param name="assemblyMasterId"></param>
        /// <returns></returns>

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
        /// <summary>
        /// Insert Booth Under Assembly, District, State
        /// </summary>
        /// <param name="stateViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("AddBooth")]
        public async Task<IActionResult> AddBooth(BoothMasterViewModel BoothMasterViewModel)
        {
            var mappedData = _mapper.Map<BoothMasterViewModel, BoothMaster>(BoothMasterViewModel);
            var result =  _EAMSService.AddBooth(mappedData);
            return Ok(result);
        }


        #endregion

        #region Event Master
        [HttpGet]
        [Route("GetEventListById")]
        public async Task<IActionResult> EventListById(string eventMasterId) 
        {
            var eventList = await _EAMSService.GetEventListById(eventMasterId);
            var mappedEvent = _mapper.Map<List<EventMasterViewModel>>(eventList);
            var data = new
            {
                count = mappedEvent.Count,
                data = mappedEvent
            };
            return Ok(data);
        }
        #endregion
    }
}
