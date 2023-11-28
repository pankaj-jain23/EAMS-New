using AutoMapper;
using CsvHelper.Configuration;
using EAMS.Helper;
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
        public async Task<IActionResult> AddState(AddStateMasterViewModel addStateMasterViewModel)
        {
            var insertstate = _mapper.Map<AddStateMasterViewModel, StateMaster>(addStateMasterViewModel);
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
        public async Task<IActionResult> AddDistrict(AddDistrictMasterViewModel addDistrictViewModel)
        {
            var mappedData = _mapper.Map<AddDistrictMasterViewModel, DistrictMaster>(addDistrictViewModel);
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
        public async Task<IActionResult> AddAssemblies(AddAssemblyMasterViewModel addAssemblyMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<AddAssemblyMasterViewModel, AssemblyMaster>(addAssemblyMasterViewModel);
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
        public async Task<IActionResult> AddSoUser(AddSectorOfficerViewModel addSectorOfficerViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<SectorOfficerMaster>(addSectorOfficerViewModel);
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

        [HttpPut]
        [Route("UpdateBooth")]
        public async Task<IActionResult> UpdateBooth(BoothMasterViewModel boothMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<BoothMaster>(boothMasterViewModel);

                var boothupdate = await _EAMSService.UpdateBooth(mappedData);

                return Ok(boothupdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpPost]
        [Route("BoothMapping")]
        public async Task<IActionResult> BoothMapping(BoothMappingViewModel boothMappingViewModel)
        {
            // Check if BoothMasterId is not null and contains values
            if (boothMappingViewModel.BoothMasterId != null && boothMappingViewModel.BoothMasterId.Any())
            {
                // Create a list to store BoothMaster objects
                List<BoothMaster> boothMasters = new List<BoothMaster>();

                // Iterate through BoothMasterId list and create BoothMaster objects
                foreach (var boothMasterId in boothMappingViewModel.BoothMasterId)
                {
                    // Create a new BoothMaster object
                    var boothMaster = new BoothMaster
                    {
                        // Set other properties of BoothMaster using your logic
                        BoothMasterId = boothMasterId,
                        StateMasterId = boothMappingViewModel.StateMasterId,
                        DistrictMasterId = boothMappingViewModel.DistrictMasterId,
                        AssemblyMasterId = boothMappingViewModel.AssemblyMasterId,
                        AssignedBy = boothMappingViewModel.AssignedBy,
                        AssignedTo = boothMappingViewModel.AssignedTo,
                        IsAssigned = boothMappingViewModel.IsAssigned,
                        // Set other properties as needed
                    };

                    // Add the BoothMaster object to the list
                    boothMasters.Add(boothMaster);
                }

                // Now you can use the list of BoothMaster objects as needed, for example, pass it to a service method
                var result = _EAMSService.BoothMapping(boothMasters);
                return Ok(new Response { Status = "Response", Message = result .Result});
                 
            }
            else
            {
                return BadRequest(new Response { Status = "Bad Request", Message = "Booth Id is Null" });
            }
        }

        #endregion

        #region Event Master
        [HttpGet]
        [Route("GetEventList")]
        public async Task<IActionResult> GetEventList()
        {
            var eventList = await _EAMSService.GetEventList();
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

        #region PC 
        [HttpGet]
        [Route("GetPCList")]
        public async Task<IActionResult> GetPCList()
        {
            var pcList = await _EAMSService.GetPCList();
            var mappedData = _mapper.Map<List<PCViewModel>>(pcList);

            var pcData = new 
            { 
                count = mappedData.Count,
                data = mappedData
            };
            return Ok(pcData);
        }
        #endregion

    }
}
