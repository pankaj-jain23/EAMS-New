using AutoMapper;
using EAMS.AuthViewModels;
using EAMS.ViewModels;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAMSAuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public EAMSAuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;

        }

        #region Register

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<UserRegistration>(registerViewModel);

                var registerResult = await _authService.RegisterAsync(mappedData);

                if (registerResult.IsSucceed)
                {
                    return Ok(registerResult);
                }
                else
                {

                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<Login>(loginViewModel);

                var loginResult = await _authService.LoginAsync(mappedData);

                if (loginResult.IsSucceed)
                {
                    return Ok(loginResult);
                }
                else
                {

                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region AddDyanmicRole && Get Role
        [HttpPost]
        [Route("AddDyanmicRole")]
        public async Task<IActionResult> AddDyanmicRole([FromBody] RolesViewModel rolesViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedData = _mapper.Map<Role>(rolesViewModel);
                var roleResult = await _authService.AddDynamicRole(mappedData);

                if (roleResult.IsSucceed)
                {
                    // Role creation succeeded, return a success status
                    return Ok(new { Message = roleResult.Message });
                }
                else
                {
                    // Role creation failed, return unauthorized status
                    return Unauthorized(new { Message = roleResult.Message });
                }
            }
            else 
            {
                return BadRequest(); 
            }
        }
        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _authService.GetRoles();

            return Ok(roles);
        }
        #endregion

        #region Validate Mobile 
        [HttpPost]
        [Route("ValidateMobile")]
        public async Task<IActionResult> ValidateMobile(ValidateMobileViewModel validateMobileViewModel)
        {
            if (ModelState.IsValid)
            { 
                var mappedData = _mapper.Map<ValidateMobile>(validateMobileViewModel);

                
                var result = await _authService.ValidateMobile(mappedData);

                switch (result.Status)
                {
                    case RequestStatusEnum.OK:
                        var response = new
                        {
                            Message = result.Message,
                            Token = result.Token,
                        };
                        return Ok(response);
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
                return BadRequest();
            }
        }

     
        #endregion

    }
}
