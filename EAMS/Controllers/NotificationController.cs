using AutoMapper;
using EAMS.ViewModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore;
using EAMS_ACore.Interfaces;
using EAMS_ACore.NotificationModels;
using EAMS_BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [Route("SendNotification")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationViewModel notificationViewModel)
        {
            var mappedData=_mapper.Map<Notification>(notificationViewModel);
            var result = await _notificationService.SendNotification(mappedData);
            
            return Ok(result);
        }
        [Route("GetNotification")]
        [HttpGet]
        public async Task<IActionResult> GetNotification()
        { 
            var result = await _notificationService.GetNotification();

            return Ok(result);
        }

        [Route("AddSMSTemplate")]
        [HttpPost]
        public async Task<IActionResult> AddSMSTemplate(SMSTemplateViewModel sMSTemplateViewModel)
        {
            var mappedData = _mapper.Map<SMSTemplate>(sMSTemplateViewModel);
            var result = await _notificationService.AddSMSTemplate(mappedData);

            return Ok(result);
        }

        [Route("GetSMSTemplates")]
        [HttpGet]
        public async Task<IActionResult> GetSMSTemplates()
        {
            var result = await _notificationService.GetSMSTemplate();

            return Ok(result);
        }
        [Route("GetSMSTemplateById")]
        [HttpGet]
        public async Task<IActionResult> GetSMSTemplateById(string SMSTemplateById)
        {
            //var mappedData = _mapper.Map<SMSSent>(SMSSentModel);
            var result = await _notificationService.GetSMSTemplateById(SMSTemplateById);
            if (result == null)
            {
                return BadRequest("Record Not Found");
            }
            else
            {
                return Ok(result);
            }
        }
        //[Route("SendtoAll")]
        //[HttpPost]
        //public async Task<IActionResult> SendtoAll(string SMSTemplateId)
        //{
        //    //var mappedData = _mapper.Map<SMSSent>(SMSSentModel);
        //    var result = await _notificationService.SendSMS(SMSTemplateId);

        //    return Ok(result);
        //}

        [Route("SendOtp")]
        [HttpPost]
        public async Task<IActionResult> SendOtp(string mobile, string otp)
        {
           
            var result = await _notificationService.SendOtp(mobile,otp);

            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateSMSTemplateById")]
        public async Task<IActionResult> UpdateSMSTemplateById(SMSTemplateViewModel sMSTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<SMSTemplate>(sMSTemplateViewModel);
                var sms_temp = await _notificationService.UpdateSMSTemplateById(mappedData);

                return Ok(sms_temp);
           
            }
            else

            {
                return BadRequest(ModelState);
            }
        }
    }
}
