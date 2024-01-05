using AutoMapper;
using EAMS.Helper;
using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Interfaces;
using EAMS_ACore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;  
using System.Security.Claims; 

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

        #region MasterUpdation Status
        [HttpPut]
        [Route("UpdateMasterStatus")]
        public async Task<IActionResult> UpdateMaster(UpdateMasterStatusViewModel updateMasterStatus)
        {
            var mappedData = _mapper.Map<UpdateMasterStatus>(updateMasterStatus);
            var isSucced = await _EAMSService.UpdateMasterStatus(mappedData);
            if (isSucced.IsSucceed)
            {
                return Ok(isSucced);
            }
            else
            {
                return BadRequest(isSucced);
            }
        }
        #endregion

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

        [HttpGet]
        [Route("GetStateById")]
        public async Task<IActionResult> GetStateById(string stateMasterId)
        {
            var stateRecord = await _EAMSService.GetStateById(stateMasterId);
            if (stateRecord != null)
            {
                var mappedData = new
                {
                    StateMasterId = stateRecord.StateMasterId,
                    StateName = stateRecord.StateName,
                    StateCode = stateRecord.StateCode,
                    IsStatus = stateRecord.StateStatus

                };
                return Ok(mappedData);
            }
            else
            {
                return NotFound($"[{stateMasterId}] not exist");
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

        [HttpGet]
        [Route("GetDistrictById")]
        public async Task<IActionResult> GetDistrictById(string distictMasterId)
        {
            var districtRecord = await _EAMSService.GetDistrictRecordById(distictMasterId);
            if (districtRecord != null)
            {
                var dataMapping = new
                {
                    StateMasterId = districtRecord.StateMasterId,
                    StateName = districtRecord.StateMaster.StateName,
                    DistrictMasterId = districtRecord.DistrictMasterId,
                    DistrictName = districtRecord.DistrictName,
                    DistrictCode = districtRecord.DistrictCode,
                    IsStatus = districtRecord.DistrictStatus

                };


                return Ok(dataMapping);
            }
            else
            {
                return NotFound($"[{distictMasterId}] not exist");
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

        [HttpGet]
        [Route("GetAssemblyById")]
        public async Task<IActionResult> GetAssemblyById(string assemblyMasterId)
        {
            var assemblyRecord = await _EAMSService.GetAssemblyById(assemblyMasterId);
            if (assemblyRecord != null)
            {
                var dataMapping = new
                {
                    StateMasterId = assemblyRecord.StateMasterId,
                    StateName = assemblyRecord.StateMaster.StateName,
                    DistrictMasterId = assemblyRecord.DistrictMaster.DistrictMasterId,
                    DistrictName = assemblyRecord.DistrictMaster.DistrictName,
                    DistrictCode = assemblyRecord.DistrictMaster.DistrictCode,
                    AssemblyMasterId = assemblyRecord.AssemblyMasterId,
                    AssemblyName = assemblyRecord.AssemblyName,
                    AssemblyCode = assemblyRecord.AssemblyCode,
                    AssemblyType = assemblyRecord.AssemblyType,
                    PcMasterId = assemblyRecord.ParliamentConstituencyMaster.PCMasterId,
                    PcName = assemblyRecord.ParliamentConstituencyMaster.PcName,
                    IsStatus = assemblyRecord.AssemblyStatus,


                };


                return Ok(dataMapping);
            }
            else
            {
                return NotFound($"[{assemblyMasterId}] not exist");
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

        [HttpGet]
        [Route("GetSectorOfficerProfile")]
        public async Task<IActionResult> GetSectorOfficerProfile()
        {
            var soIdClaim = User.Claims.FirstOrDefault(c => c.Type == "SoId");
            if (soIdClaim == null)
            {
                // Handle the case where the SoId claim is not present
                return BadRequest("SoId claim not found.");
            }

            var soId = soIdClaim.Value;
            var soList = await _EAMSService.GetSectorOfficerProfile(soId);  // Corrected to await the asynchronous method
            if (soList != null)
            {
                var data = new
                {
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
                Assigned = mappedData.OrderBy(p => Int32.Parse(p.BoothCode_No)),
                Unassigned = unAssignedMappedData
            };
            return Ok(data);
        }

        [HttpGet]
        [Route("GetSOById")]
        public async Task<IActionResult> GetSOById(string soMasterId)
        {
            var soRecord = await _EAMSService.GetSOById(soMasterId);
            if (soRecord != null)
            {



                return Ok(soRecord);
            }
            else
            {
                return NotFound($"[{soMasterId}] not exist");
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
            if (stateMasterId != null && districtMasterId != null && assemblyMasterId != null)
            {
                var boothList = await _EAMSService.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
                if (boothList != null)
                {
                    var data = new
                    {
                        count = boothList.Count,
                        data = boothList.ToList()
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

        [HttpGet]
        [Route("GetBoothListForARO")]
        [Authorize]
        public async Task<IActionResult> GetBoothListForARO()
        {
            Claim stateMaster = User.Claims.FirstOrDefault(c => c.Type == "StateMasterId");
            Claim districtMaster = User.Claims.FirstOrDefault(c => c.Type == "DistrictMasterId");
            Claim assemblyMaster = User.Claims.FirstOrDefault(c => c.Type == "AssemblyMasterId");
            string stateMasterId = stateMaster.Value;
            string districtMasterId = districtMaster.Value;
            string assemblyMasterId = assemblyMaster.Value;

            if (stateMasterId != null && districtMasterId != null && assemblyMasterId != null)
            {
                var boothList = await _EAMSService.GetBoothListById(stateMasterId, districtMasterId, assemblyMasterId);  // Corrected to await the asynchronous method
                if (boothList != null)
                {
                    var data = new
                    {
                        count = boothList.Count,
                        data = boothList.ToList()
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





        [HttpGet]
        [Route("GetSectorOfficersListforARO")]
        [Authorize]
        public async Task<IActionResult> GetSectorOfficersListforARO()
        {
            Claim stateMaster = User.Claims.FirstOrDefault(c => c.Type == "StateMasterId");
            Claim districtMaster = User.Claims.FirstOrDefault(c => c.Type == "DistrictMasterId");
            Claim assemblyMaster = User.Claims.FirstOrDefault(c => c.Type == "AssemblyMasterId");
            string stateMasterId = stateMaster.Value;
            string districtMasterId = districtMaster.Value;
            string assemblyMasterId = assemblyMaster.Value;

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
                if (boothMappingViewModel.BoothMasterId != null && boothMappingViewModel.BoothMasterId.Any() && boothMappingViewModel.IsAssigned == true && !string.IsNullOrWhiteSpace(boothMappingViewModel.AssignedTo))
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
                    return BadRequest(new Response { Status = RequestStatusEnum.BadRequest, Message = "Please Check the Parameters" });
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

        [HttpGet]
        [Route("GetBoothById")]
        public async Task<IActionResult> GetBoothById(string boothMasterId)
        {
            var boothRecord = await _EAMSService.GetBoothById(boothMasterId);
            if (boothRecord != null)
            {
                var dataMapping = new
                {
                    StateMasterId = boothRecord.StateMasterId,
                    StateName = boothRecord.StateMaster.StateName,
                    DistrictMasterId = boothRecord.DistrictMaster.DistrictMasterId,
                    DistrictName = boothRecord.DistrictMaster.DistrictName,
                    DistrictCode = boothRecord.DistrictMaster.DistrictCode,
                    AssemblyMasterId = boothRecord.AssemblyMasterId,
                    AssemblyName = boothRecord.AssemblyMaster.AssemblyName,
                    AssemblyCode = boothRecord.AssemblyMaster.AssemblyCode,
                    AssemblyType = boothRecord.AssemblyMaster.AssemblyType,
                    BoothMasterId = boothRecord.BoothMasterId,
                    BoothName = boothRecord.BoothName,
                    BoothCode_No = boothRecord.BoothCode_No,
                    BoothNoAuxy = boothRecord.BoothNoAuxy,
                    TotalVoters = boothRecord.TotalVoters,
                    IsStatus = boothRecord.BoothStatus

                };


                return Ok(dataMapping);
            }
            else
            {
                return NotFound($"[{boothMasterId}] not exist");
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
        [Route("UpdateEventStaus")]
        public async Task<IActionResult> UpdateEventStaus(UpdateEventStatusViewModel updateEventStatusViewModel)
        {
            var mappedData = _mapper.Map<EventMaster>(updateEventStatusViewModel);
            var isSucced = await _EAMSService.UpdateEventStaus(mappedData);
            if (isSucced.IsSucceed)
            {
                return Ok(isSucced);
            }
            else
            {
                return BadRequest(isSucced);
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

        [HttpGet]
        [Route("GetBoothListByEventId")]
        public async Task<IActionResult> GetBoothListByEventId(string eventId)
        {
            var soIdClaim = User.Claims.FirstOrDefault(c => c.Type == "SoId");

            if (soIdClaim == null)
            {
                // Handle the case where the SoId claim is not present
                return BadRequest("SoId claim not found.");
            }

            var soId = soIdClaim.Value;
            var eventWiseBoothList = await _EAMSService.GetBoothListByEventId(eventId, soId);

            if (eventWiseBoothList != null)
            {
                var data = new
                {
                    count = eventWiseBoothList.Count,
                    data = eventWiseBoothList.OrderBy(p => Int32.Parse(p.BoothCode))
                };
                return Ok(data);
            }
            else
            {
                return BadRequest("No Record Found");
            }

        }

        [HttpGet]
        [Route("GetBoothStatusByEventIdforARO")]
        public async Task<IActionResult> GetBoothStatusByEventIdforARO(string eventId, string soId)
        {

            var eventWiseBoothList = await _EAMSService.GetBoothListByEventId(eventId, soId);

            if (eventWiseBoothList != null)
            {
                var data = new
                {
                    count = eventWiseBoothList.Count,
                    data = eventWiseBoothList.OrderBy(p => Int32.Parse(p.BoothCode))
                };
                return Ok(data);
            }
            else
            {
                return BadRequest("No Record Found");
            }

        }




        [HttpGet]
        [Route("GetBoothStatusforARO")]
        public async Task<IActionResult> GetBoothStatusforARO(string assemblyMasterId, string boothMasterId)
        {

            var eventWiseBoothList = await _EAMSService.GetBoothStatusforARO(assemblyMasterId, boothMasterId);

            if (eventWiseBoothList != null)
            {
                var data = new
                {
                    count = eventWiseBoothList.Count,
                    data = eventWiseBoothList
                };
                return Ok(data);
            }
            else
            {
                return BadRequest("No Record Found");
            }

        }


        #endregion

        #region PC 
        [HttpGet]
        [Route("GetPCList")]
        public async Task<IActionResult> GetPCList(string stateMasterId)
        {
            var pcList = await _EAMSService.GetPCList(stateMasterId);
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


        [HttpPost]
        [Route("AddPC")]
        public async Task<IActionResult> AddPC(PCViewModel addPc)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<PCViewModel, ParliamentConstituencyMaster>(addPc);
                var result = await _EAMSService.AddPC(mappedData);
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
        [Route("UpdatePC")]
        public async Task<IActionResult> UpdatePC(PCViewModel pcViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<PCViewModel, ParliamentConstituencyMaster>(pcViewModel);
                var result = await _EAMSService.UpdatePC(mappedData);
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
        [Route("GetAssemblyByPCId")]
        public async Task<IActionResult> GetAssemblyByPCId(string stateMasterid, string PcMasterId)
        {
            var asembList = await _EAMSService.GetAssemblyByPCId(stateMasterid, PcMasterId);
            var mappedData = _mapper.Map<List<AssemblyMasterViewModel>>(asembList);
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
        [HttpGet]
        [Route("GetAssemblyByDistrictId")]
        public async Task<IActionResult> GetAssemblyByDistrictId(string stateMasterid, string districtMasterId)
        {
            var asembList = await _EAMSService.GetAssemblyByDistrictId(stateMasterid, districtMasterId);
            var mappedData = _mapper.Map<List<AssemblyMasterViewModel>>(asembList);
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
                    var result = await PartyDispatch(electionInfoViewModel);
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

                case 2:
                    var result_part_reach = await PartyReached(electionInfoViewModel);
                    switch (result_part_reach.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(result_part_reach.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(result_part_reach.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(result_part_reach.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }
                case 3:
                    var result_setup_polling = await SetupPollingStation(electionInfoViewModel);
                    switch (result_setup_polling.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(result_setup_polling.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(result_setup_polling.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(result_setup_polling.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }


                case 4:
                    var result_mock_poll = await MockPollDone(electionInfoViewModel);
                    switch (result_mock_poll.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(result_mock_poll.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(result_mock_poll.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(result_mock_poll.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 5:
                    var res_pollstarted = await PollStarted(electionInfoViewModel);
                    switch (res_pollstarted.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_pollstarted.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_pollstarted.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_pollstarted.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 7:
                    var res_voter_in_queue = await VoterInQueue(electionInfoViewModel);
                    switch (res_voter_in_queue.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_voter_in_queue.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_voter_in_queue.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_voter_in_queue.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 8:
                    var res_final_votes = await FinalVotes(electionInfoViewModel);
                    switch (res_final_votes.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_final_votes.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_final_votes.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_final_votes.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 9:
                    var res_poll_ended = await PollEnded(electionInfoViewModel);
                    switch (res_poll_ended.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_poll_ended.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_poll_ended.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_poll_ended.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }
                case 10:
                    var res_evm_swicthoff = await MCEVM(electionInfoViewModel);
                    switch (res_evm_swicthoff.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_evm_swicthoff.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_evm_swicthoff.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_evm_swicthoff.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }
                case 11:
                    var res_party_departed = await PartyDeparted(electionInfoViewModel);
                    switch (res_party_departed.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_party_departed.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_party_departed.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_party_departed.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 12:
                    var res_party_reachd_collection_centre = await PartyReachedCollectionCentre(electionInfoViewModel);
                    switch (res_party_reachd_collection_centre.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_party_reachd_collection_centre.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_party_reachd_collection_centre.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_party_reachd_collection_centre.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }

                case 13:
                    var res_evm_deposited = await EVMDeposited(electionInfoViewModel);
                    switch (res_evm_deposited.Status)
                    {
                        case RequestStatusEnum.OK:
                            return Ok(res_evm_deposited.Message);
                        case RequestStatusEnum.BadRequest:
                            return BadRequest(res_evm_deposited.Message);
                        case RequestStatusEnum.NotFound:
                            return NotFound(res_evm_deposited.Message);

                        default:
                            return StatusCode(500, "Internal Server Error");
                    }


                default:
                    // Handle the case when EventMasterId doesn't match any known case
                    return BadRequest("Invalid EventMasterId");
            }

        }

        private async Task<Response> PartyDispatch(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPartyDispatched = electionInfoViewModel.EventStatus,

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        private async Task<Response> PartyReached(ElectionInfoViewModel electionInfoViewModel)
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
            return result;
        }

        private async Task<Response> SetupPollingStation(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsSetupOfPolling = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        private async Task<Response> MockPollDone(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsMockPollDone = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        private async Task<Response> PollStarted(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPollStarted = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        [HttpGet]
        [Route("GetLastUpdatedPollDetail")]
        public async Task<IActionResult> GetLastUpdatedPollDetail(string boothMasterId)
        {
            int voterturnotEventId = 6;
            var result = await _EAMSService.GetLastUpdatedPollDetail(boothMasterId, voterturnotEventId);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("AddVoterTurnOut")]
        public async Task<IActionResult> AddVoterTurnOut(string boothMasterId, string voterValue)
        {
            int eventTurnoutd = 6;
            var result = await _EAMSService.AddVoterTurnOut(boothMasterId, eventTurnoutd, voterValue);
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


        [HttpGet]
        [Route("GetVoterInQueue")]
        public async Task<IActionResult> GetVoterInQueue(string boothMasterId)
        {

            var result = await _EAMSService.GetVoterInQueue(boothMasterId);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetFinalVotes")]
        public async Task<IActionResult> GetFinalVotes(string boothMasterId)
        {

            var result = await _EAMSService.GetFinalVotes(boothMasterId);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
        private async Task<Response> VoterInQueue(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                VoterInQueue = Convert.ToInt32(electionInfoViewModel.VoterInQueue
                )

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }
        private async Task<Response> FinalVotes(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                FinalTVote = Convert.ToInt32(electionInfoViewModel.FinalVotes)

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }
        private async Task<Response> PollEnded(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPollEnded = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        private async Task<Response> MCEVM(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsMCESwitchOff = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }
        private async Task<Response> PartyDeparted(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPartyDeparted = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }
        private async Task<Response> PartyReachedCollectionCentre(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsPartyReachedCollectionCenter = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }
        private async Task<Response> EVMDeposited(ElectionInfoViewModel electionInfoViewModel)
        {
            ElectionInfoMaster electionInfoMaster = new ElectionInfoMaster()
            {
                StateMasterId = electionInfoViewModel.StateMasterId,
                DistrictMasterId = electionInfoViewModel.DistrictMasterId,
                AssemblyMasterId = electionInfoViewModel.AssemblyMasterId,
                BoothMasterId = electionInfoViewModel.BoothMasterId,
                EventMasterId = electionInfoViewModel.EventMasterId,
                IsEVMDeposited = electionInfoViewModel.EventStatus

            };
            var result = await _EAMSService.EventActivity(electionInfoMaster);
            return result;
        }

        #region Event Count for Dashboard
        [HttpGet]
        [Route("GetDistrictWiseEventListById")]
        [Authorize (Roles ="ECI,SuperAdmin,StateAdmin,DistrictAdmin") ]
        public async Task<IActionResult> EventListDistrictWiseById(string stateId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
           
            var eventDistrictWiseList = await _EAMSService.GetEventListDistrictWiseById(stateId, userId);
            if (eventDistrictWiseList is not null)
                return Ok(eventDistrictWiseList);
            else
                return NotFound(); 
        }
        [HttpGet]
        [Route("GetAssemblyWiseEventListById")]
        [Authorize(Roles = "ECI,SuperAdmin,StateAdmin,DistrictAdmin")]
        public async Task<IActionResult> EventListAssemblyWiseById(string stateId, string districtId)
        {
            var eventAssemblyList = await _EAMSService.GetEventListAssemblyWiseById(stateId, districtId);
            if (eventAssemblyList is not null)
                return Ok(eventAssemblyList);
            else
                return NotFound();
        }
        [HttpGet]   
        [Route("GetBoothWiseEventListById")]        
        public async Task<IActionResult> EventListBoothWiseById(string stateId, string districtId, string assemblyId)
        {
            var eventBoothList = await _EAMSService.GetEventListBoothWiseById(stateId, districtId, assemblyId);
            if (eventBoothList is not null)
                return Ok(eventBoothList);
            else
                return NotFound();
        }
        #endregion

        #endregion

        #region Event Wise Booth Status
        [HttpGet]
        [Route("EventWiseBoothStatus")]
        // [Authorize(Roles = "SO")]
        public async Task<IActionResult> EventWiseBoothStatus()
        {
            var soIdClaim = User.Claims.FirstOrDefault(c => c.Type == "SoId");
            if (soIdClaim == null)
            {
                // Handle the case where the SoId claim is not present
                return BadRequest("SoId claim not found.");
            }

            var soId = soIdClaim.Value;
            var result = await _EAMSService.EventWiseBoothStatus(soId);

            return Ok(result);
        }

        #endregion

        #region Event Slot Management
        [HttpPost]
        [Route("AddEventSlot")]
        public async Task<IActionResult> AddEventSlot(SlotManagementViewModel slotManagementViewModel)
        {
            // Ensure the slot list is not null or empty
            if (slotManagementViewModel?.slotTimes != null && slotManagementViewModel.slotTimes.Any())
            {
                // Set the isLastSlot property for the last slot to true
                var lastSlot = slotManagementViewModel.slotTimes.Last();
                lastSlot.IsLastSlot = true;
            }

            var slotManagements = _mapper.Map<List<SlotManagementMaster>>(slotManagementViewModel);

            var result = await _EAMSService.AddEventSlot(slotManagements);

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

        [HttpGet]
        [Route("GetEventSlotListById")]
        public async Task<IActionResult> GetEventSlotList(int stateMasterId, int EventId)
        {
            var result = await _EAMSService.GetEventSlotList();
            if (result is not null)
            {

                return Ok(result);
            }
            else
            {

                return BadRequest();
            }


        }
        #endregion

        #region UserList
        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList(string userName, string type)
        {
            var userList = await _EAMSService.GetUserList(userName, type);
            var data = new
            {
                count = userList.Count,
                data = userList
            };
            return Ok(data);

        }

      
        

        #endregion

        #region PollInterruption

        [HttpPost]
        [Route("AddPollInterruption")]        
        public async Task<IActionResult> AddPollInterruption(InterruptionViewModel interruptionViewModel)
        {
            var mappedData = _mapper.Map<PollInterruption>(interruptionViewModel);
            var result = await _EAMSService.AddPollInterruption(mappedData);

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


        [HttpGet]
        [Route("GetPollInterruptionbyId")]
        public async Task<IActionResult> GetPollInterruptionbyId(string boothMasterId)
        {
            PollInterruption? data= await _EAMSService.GetPollInterruptionbyId(boothMasterId);
            PollInterruption pollInterruptionData = new PollInterruption();
            if (data == null)
            {
                
                pollInterruptionData.BoothMasterId = Convert.ToInt32(boothMasterId);
                pollInterruptionData.Flag = "Fresh";
                pollInterruptionData.IsPollInterrupted = false;
               
            }
            else
            {
                pollInterruptionData = data;

            }

            return Ok(pollInterruptionData);

        }


        [HttpGet]
        [Route("GetPollInterruptionHistoryById")]
        public async Task<IActionResult> GetPollInterruptionHistoryById(string boothMasterId)
        {
            var data = await _EAMSService.GetPollInterruptionHistoryById(boothMasterId);
            return Ok(data);

        }

        [HttpGet]
        [Route("GetPollInterruptionDashboard")]
        public async Task<IActionResult> GetPollInterruptionDashboard(string StateId)
        {
            var data_record = await  _EAMSService.GetPollInterruptionDashboard(StateId);
            var filtered_pending = data_record.Where(p => p.isPollInterrupted == true).ToList();
            var filtered_resolved = data_record.Where(p => p.isPollInterrupted == false).ToList();
            if (data_record != null)
            {
                var data = new
                {
                    totalInterruptions= data_record.Count,
                    Pending = filtered_pending.Count,
                    Resolved = filtered_resolved.Count,
                    data = filtered_pending
                };
                return Ok(data);

            }
            else
            {
                return NotFound("Data Not Found");

            }
           
        }
            #endregion

        }
}