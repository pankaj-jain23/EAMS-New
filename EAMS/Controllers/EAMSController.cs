using AutoMapper;
using CsvHelper.Configuration;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.Interfaces;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAMSController : ControllerBase
    {
        private readonly IEamsService _EAMSService;
        private readonly IMapper _mapper;

        public EAMSController(IEamsService eamsService, IMapper mapper)
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
                count = mappedData.Count,
                data = mappedData
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
                count = mappedData.Count,
                data = mappedData
            };
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateDistrictById")]
        public async Task<IActionResult> UpdateDistrictById(DistrictMasterViewModel districtViewModel)
        {
            var mappedData = _mapper.Map<DistrictMasterViewModel, DistrictMaster>(districtViewModel);
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
            var assemblyList = await _EAMSService.GetAssemblies(stateId, districtId);  // Corrected to await the asynchronous method
            var data = new
            {
                count = assemblyList.Count,
                data = assemblyList
            };
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateAssembliesById")]
        public async Task<IActionResult> UpdateAssembliesById(AssemblyMasterViewModel assemblyViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<AssemblyMasterViewModel, AssemblyMaster>(assemblyViewModel);
                var update = _EAMSService.AddAssemblies(mappedData);
                return Ok(update);
            }
            else
            { 
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddAssemblies")]

        public async Task<IActionResult> AddAssemblies(AssemblyMasterViewModel assemblyMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<AssemblyMasterViewModel, AssemblyMaster>(assemblyMasterViewModel);
            var add = _EAMSService.AddAssemblies(mappedData);
            return Ok(add);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region  SO Master
        [HttpGet]
        [Route("GetSectorOfficersListById")]
        public async Task<IActionResult> SectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            var soList = await _EAMSService.GetSectorOfficersListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
            var data = new
            {
                count = soList.Count,
                data = soList
            };
            return Ok(data);
        }
        [HttpPost]
        [Route("AddSOUser")]
        public async Task<IActionResult> AddSoUser(SectorOfficerViewModel sectorOfficerViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<SectorOfficerMaster>(sectorOfficerViewModel);
                var soUser = await _EAMSService.AddSectorOfficer(mappedData);

                return Ok(soUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [Route("UpdateSOUser")]
        public async Task<IActionResult> UpdateSOUser(SectorOfficerViewModel sectorOfficerViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<SectorOfficerMaster>(sectorOfficerViewModel);

                var soUser = await _EAMSService.UpdateSectorOfficer(mappedData);

                return Ok(soUser);
            }
            else
            {
                return BadRequest(ModelState);
            }
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
        public async Task<IActionResult> BoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            var boothList = await _EAMSService.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
            var data = new
            {
                count = boothList.Count,
                data = boothList
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
            var result = _EAMSService.AddBooth(mappedData);
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

        [HttpPut]
        [Route("UpdateEventById")]
        public async Task<IActionResult> UpdateEventById(EventMasterViewModel eventMaster)
        {
            var mappedeventData = _mapper.Map<EventMasterViewModel, EventMaster>(eventMaster);
            var eventUplist = _EAMSService.UpdateEventById(mappedeventData);
            return Ok(eventUplist);
        }

        #endregion
    }
}
