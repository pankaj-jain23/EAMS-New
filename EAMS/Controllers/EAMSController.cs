using AutoMapper;
using CsvHelper.Configuration;
using EAMS.Helper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
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
