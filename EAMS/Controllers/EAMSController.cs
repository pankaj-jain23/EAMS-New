using EAMS.ViewModels;
using EAMS_ACore;
using EAMS_ACore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAMSController : ControllerBase
    {
        private readonly IEamsService _EAMSService; 
         
        public EAMSController(IEamsService eamsService )
        {
            _EAMSService = eamsService;
            
        }

        #region State master
        [HttpGet]
        [Route("StateList")]
        public async Task<IActionResult> StateList()
        {
            var st = await _EAMSService.GetState();
            return Ok(st);
        }


        [HttpPut]
        [Route("UpdateStateById")]
        public async Task<IActionResult> UpdateStateById(StateViewModel stateViewModel)
        {
            StateMaster stateMaster = new StateMaster()
            {
                StateMasterId = stateViewModel.StateId,
                StateCode = stateViewModel.StateCode,
                StateName = stateViewModel.Name
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
            var sd = await _EAMSService.GetDistrictById(stateMasterId);   
            return Ok(sd);
        }

        [HttpPut]
        [Route("UpdateDistrictById")]
        public async Task<IActionResult> UpdateDistrictById(DistrictMasterViewModel districtViewModel)
        {
            DistrictMaster districtMaster = new DistrictMaster()
            {
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
            var st = await _EAMSService.GetAssemblies(stateId,districtId);  // Corrected to await the asynchronous method
            return Ok(st);
        }

        #endregion

        #region  SO Master

        [HttpGet]
        [Route("GetSectorOfficersListById")]
        public async Task<IActionResult> SectorOfficersListById(string stateId)
        {
            var st = await _EAMSService.GetSectorOfficersListById(stateId);  // Corrected to await the asynchronous method
            return Ok(st);
        }
        #endregion

        #region Booth Master

        [HttpGet]
        [Route("GetBoothListById")]
        public async Task<IActionResult> BoothListById(string stateMasterId,string districtMasterId, string assemblyMasterId)
        {
            var st = await _EAMSService.GetBoothListById(stateMasterId,districtMasterId,assemblyMasterId);  // Corrected to await the asynchronous method
            return Ok(st);
        }

        #endregion

         
       
    }
}
