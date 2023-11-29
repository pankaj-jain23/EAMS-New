using AutoMapper;
using EAMS.AuthViewModels;
using EAMS.ViewModels;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
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

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationViewModel registerViewModel)
        {
            var mappedData = _mapper.Map<UserRegistration>(registerViewModel);

            var registerResult = await _authService.RegisterAsync(mappedData);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var mappedData = _mapper.Map<Login>(loginViewModel);

            var loginResult = await _authService.LoginAsync(mappedData);

            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized();
        }
    }
}
