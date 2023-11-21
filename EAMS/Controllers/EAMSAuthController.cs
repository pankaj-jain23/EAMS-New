using EAMS_ACore.AuthInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAMSAuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public EAMSAuthController(IAuthService authService)
        {
            _authService = authService;
        }
    }
}
