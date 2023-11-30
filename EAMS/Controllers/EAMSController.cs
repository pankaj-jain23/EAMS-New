using AutoMapper;
using CsvHelper.Configuration;
using EAMS.Helper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mono.TextTemplating;
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
            if (ModelState.IsValid)
            {
                StateMaster stateMaster = new StateMaster()
                {
                    StateMasterId = stateViewModel.StateId,
                    StateCode = stateViewModel.StateCode,
                    StateName = stateViewModel.StateName
                };
                var state = await _EAMSService.UpdateStateById(stateMaster);
                switch (state.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(state.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(state.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(state.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }



            }
            else

            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("AddState")]
        public async Task<IActionResult> AddState(AddStateMasterViewModel addStateMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var insertstate = _mapper.Map<AddStateMasterViewModel, StateMaster>(addStateMasterViewModel);
                var result = await _EAMSService.AddState(insertstate);

                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }



            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        #endregion

        #region District Master
        [HttpGet]
        [Route("DistrictList")]
        public async Task<IActionResult> DistrictListById(string stateMasterId)
        {
            if (stateMasterId != null)
            {
                var districtList = await _EAMSService.GetDistrictById(stateMasterId);
                var mappedData = _mapper.Map<List<DistrictMasterViewModel>>(districtList);
                if (stateMasterId != null)
                {

                    var data = new
                    {
                        count = mappedData.Count,
                        data = mappedData
                    };
                    return Ok(data);
                }
                else
                {
                    return NotFound("Data Not Found");
                }
            }
            else
            {
                return BadRequest(stateMasterId + "is null");
            }
        }

        [HttpPut]
        [Route("UpdateDistrictById")]
        public async Task<IActionResult> UpdateDistrictById(DistrictMasterViewModel districtViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<DistrictMasterViewModel, DistrictMaster>(districtViewModel);
                var result = await _EAMSService.UpdateDistrictById(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        [Route("AddDistrict")]
        public async Task<IActionResult> AddDistrict(AddDistrictMasterViewModel addDistrictViewModel)
        {
            if (ModelState.IsValid)
            {

                var mappedData = _mapper.Map<AddDistrictMasterViewModel, DistrictMaster>(addDistrictViewModel);
                var result = await _EAMSService.AddDistrict(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Assembliy Master

        [HttpGet]
        [Route("GetAssembliesListById")]
        public async Task<IActionResult> AssembliesListById(string stateId, string districtId)
        {
            if (stateId != null && districtId != null)
            {
                var assemblyList = await _EAMSService.GetAssemblies(stateId, districtId);  // Corrected to await the asynchronous method
                if (assemblyList != null)
                {
                    var data = new
                    {
                        count = assemblyList.Count,
                        data = assemblyList
                    };
                    return Ok(data);
                }
                else
                {
                    return NotFound("Data Not Found");
                }
            }
            else
            {
                return BadRequest("State and District Master Id's cannot be null");
            }


        }

        [HttpPut]
        [Route("UpdateAssembliesById")]
        public async Task<IActionResult> UpdateAssembliesById(AssemblyMasterViewModel assemblyViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<AssemblyMasterViewModel, AssemblyMaster>(assemblyViewModel);
                var result = await _EAMSService.UpdateAssembliesById(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("AddAssemblies")]
        public async Task<IActionResult> AddAssemblies(AddAssemblyMasterViewModel addAssemblyMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<AddAssemblyMasterViewModel, AssemblyMaster>(addAssemblyMasterViewModel);
                var result = await _EAMSService.AddAssemblies(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region  SO Master
        [HttpGet]
        [Route("GetSectorOfficersListById")]
        public async Task<IActionResult> SectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            var soList = await _EAMSService.GetSectorOfficersListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
            if (soList != null)
            {
                var data = new
                {
                    count = soList.Count,
                    data = soList
                };
                return Ok(data);
            }
            else
            {
                return BadRequest("No Record Found");
            }

        }
        [HttpPost]
        [Route("AddSOUser")]
        public async Task<IActionResult> AddSoUser(AddSectorOfficerViewModel addSectorOfficerViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<SectorOfficerMaster>(addSectorOfficerViewModel);
                var result = await _EAMSService.AddSectorOfficer(mappedData);

                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
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

                var result = await _EAMSService.UpdateSectorOfficer(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }


            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [Route("GetBoothListBySoId")]
        public async Task<IActionResult> GetBoothListBySoId(string stateMasterId, string districtMasterId, string assemblyMasterId, string soId)
        {
            var boothList = await _EAMSService.GetBoothListBySoId(stateMasterId, districtMasterId, assemblyMasterId, soId);  // Corrected to await the asynchronous method
            var mappedData = _mapper.Map<List<SectorOfficerBoothViewModel>>(boothList);
            var getUnassignedBoothList = await _EAMSService.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
            var unAssignedMappedData = _mapper.Map<List<CombinedMasterViewModel>>(getUnassignedBoothList);
            var data = new
            {
                AssignedCount = mappedData.Count,
                UnAssignedCount = unAssignedMappedData.Count,
                Assigned = mappedData,
                Unassigned = unAssignedMappedData
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
        public async Task<IActionResult> BoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {
            if (stateMasterId != null && districtMasterId != null && assemblyMasterId != null)
            {
                var boothList = await _EAMSService.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
                if (boothList != null)
                {
                    var data = new
                    {
                        count = boothList.Count,
                        data = boothList
                    };
                    return Ok(data);

                }
                else
                {
                    return NotFound("Data Not Found");

                }
            }
            else
            {

                return BadRequest("State, District and Assembly Master Id's cannot be null");
            }
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
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<BoothMasterViewModel, BoothMaster>(BoothMasterViewModel);
                var result = await _EAMSService.AddBooth(mappedData);
                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
            }
            else
            {
                return BadRequest(ModelState);

            }
        }

        [HttpPut]
        [Route("UpdateBooth")]
        public async Task<IActionResult> UpdateBooth(BoothMasterViewModel boothMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<BoothMaster>(boothMasterViewModel);

                var result = await _EAMSService.UpdateBooth(mappedData);

                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
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
            if (ModelState.IsValid)
            {
                if (boothMappingViewModel.BoothMasterId != null && boothMappingViewModel.BoothMasterId.Any() && boothMappingViewModel.IsAssigned == true)
                {
                    List<BoothMaster> boothMasters = new List<BoothMaster>();

                    foreach (var boothMasterId in boothMappingViewModel.BoothMasterId)
                    {
                        var boothMaster = new BoothMaster
                        {
                            BoothMasterId = boothMasterId,
                            StateMasterId = boothMappingViewModel.StateMasterId,
                            DistrictMasterId = boothMappingViewModel.DistrictMasterId,
                            AssemblyMasterId = boothMappingViewModel.AssemblyMasterId,
                            AssignedBy = boothMappingViewModel.AssignedBy,
                            AssignedTo = boothMappingViewModel.AssignedTo,
                            IsAssigned = boothMappingViewModel.IsAssigned,
                        };

                        boothMasters.Add(boothMaster);
                    }

                    // Assuming _EAMSService.BoothMapping is an asynchronous method returning Task<Response>
                    var result = await _EAMSService.BoothMapping(boothMasters);
                    switch (result.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(result.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(result.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(result.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }
                }
                else
                {
                    return BadRequest(new Response { Status = RequestStatusEnum.BadRequest, Message = "Booth Id is Null" });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("ReleaseBooth")]
        public async Task<IActionResult> ReleaseBooth(BoothReleaseViewModel boothReleaseViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mapperdata = _mapper.Map<BoothMaster>(boothReleaseViewModel);
                    var boothReleaseResponse = await _EAMSService.ReleaseBooth(mapperdata);

                    switch (boothReleaseResponse.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(boothReleaseResponse.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(boothReleaseResponse.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(boothReleaseResponse.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        #endregion

        #region Event Master
        [HttpGet]
        [Route("GetEventList")]
        public async Task<IActionResult> GetEventList()
        {
            var eventList = await _EAMSService.GetEventList();
            if (eventList != null)
            {
                var mappedEvent = _mapper.Map<List<EventMasterViewModel>>(eventList);
                if (mappedEvent != null)
                {
                    var data = new
                    {
                        count = mappedEvent.Count,
                        data = mappedEvent
                    };
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No Record Found");
                }
            }
            else
            {
                return BadRequest("No Record Found");
            }
        }

        [HttpPut]
        [Route("UpdateEventById")]
        public async Task<IActionResult> UpdateEventById(EventMasterViewModel eventMaster)
        {
            if (ModelState.IsValid)
            {
                var mappedeventData = _mapper.Map<EventMasterViewModel, EventMaster>(eventMaster);
                var result = await _EAMSService.UpdateEventById(mappedeventData);

                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        return Ok(result.Message);
                    case RequestStatusEnum.BadRequest:
                        return BadRequest(result.Message);
                    case RequestStatusEnum.NotFound:
                        return NotFound(result.Message);

                    default:
                        return StatusCode(500, "Internal Server Error");
                }
            }
            else

            {
                return BadRequest(ModelState);

            }
        }



        #endregion

        #region PC 
        [HttpGet]
        [Route("GetPCList")]
        public async Task<IActionResult> GetPCList()
        {
            var pcList = await _EAMSService.GetPCList();
            var mappedData = _mapper.Map<List<PCViewModel>>(pcList);
            if (mappedData != null)
            {
                var pcData = new
                {
                    count = mappedData.Count,
                    data = mappedData
                };
                return Ok(pcData);
            }
            else
            {
                return BadRequest("No Record Found");
            }
        }
        #endregion

        #region Event Activity

        [HttpPost]
        [Route("EventActivity")]
        public async Task<IActionResult> EventActivity(ElectionInfoViewModel electionInfoViewModel)
        {
            switch (electionInfoViewModel.EventMasterId)
            {
                case 1:
                    await PartyDispatch(electionInfoViewModel);
                    break;
                case 2:
                    await PartyReached(electionInfoViewModel);
                    break;


                default:
                    // Handle the case when EventMasterId doesn't match any known case
                    return BadRequest("Invalid EventMasterId");
            }

            return Ok();
        }

        private async Task PartyDispatch(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPartyDispatched = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);


        }

        private async Task PartyReached(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPartyReached = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);

        }

        #endregion


    }
}
