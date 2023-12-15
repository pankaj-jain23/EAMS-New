using AutoMapper;
using EAMS.AuthViewModels;
using EAMS.ViewModels;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using Microsoft.AspNetCore.Authorization;
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
        [Route("registeration")]
        public async Task<IActionResult> Register(UserRegistrationViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var mappedData = _mapper.Map<UserRegistration>(registerViewModel);
                var roleId=registerViewModel.RoleId;
                var registerResult = await _authService.RegisterAsync(mappedData, roleId);
                if (registerResult.IsSucceed == false)
                {
                    return BadRequest(registerResult.Message);
                }
                else
                {
                    return Ok(registerResult.Message);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion

        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var mappedData = _mapper.Map<Login>(loginViewModel);

                var loginResult = await _authService.LoginAsync(mappedData);

                if (loginResult.IsSucceed == false)
                    return BadRequest(loginResult.Message);
                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                            AccessToken = result.AccessToken,
                            RefreshToken = result.RefreshToken,
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

        #region Refresh Token

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(GetRefreshTokenViewModel refreshTokenViewModel)
        {
            try
            {
                if (refreshTokenViewModel is null)
                {
                    return BadRequest("Invalid client request");
                }
                var mapped = _mapper.Map<GetRefreshToken>(refreshTokenViewModel);

                var result = await _authService.GetRefreshToken(mapped);
                if (result.IsSucceed is false)
                    return BadRequest(result.Message);
                else
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region CreateSoPin
        [HttpPost]
        [Route("CreateSOPin")]
        [Authorize (Roles ="SO")]
        public async Task<IActionResult> CreateSOPin(CreateSOPinViewModel createSOPinViewModel)
        {
            // Retrieve SoId from the claims
            var soIdClaim = User.Claims.FirstOrDefault(c => c.Type == "SoId");
            if (soIdClaim == null)
            {
                // Handle the case where the SoId claim is not present
                return BadRequest("SoId claim not found.");
            }

            var soID = soIdClaim.Value;

            var mappedData = _mapper.Map<CreateSOPin>(createSOPinViewModel);
            var result =await _authService.CreateSOPin(mappedData, soID);
            if(result.IsSucceed is true)
            {
                return Ok(result);  
            }
            else
            {
                return BadRequest(result);
            }
             
        }


        //[HttpPost]
        //[Route("ForgetSOPin")]
        //public async Task<IActionResult> ForgetSOPin()
        //{
        //    return Ok();
        //}
        #endregion
    }
}
