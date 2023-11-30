using EAMS.Helper;
using EAMS_ACore.AuthInterfaces;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IAuthRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EAMS_BLL.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        { 
            _configuration = configuration;
            _authRepository = authRepository;
        }

        #region AddDynamicRole && Get Role
        public async Task<AuthServiceResponse> AddDynamicRole(Role role)
        {
             
            return  await _authRepository.AddDynamicRole(role);
        }

        public async Task<List<Role>> GetRoles()
        {
             return await _authRepository.GetRoles();
        }
        #endregion

        #region Login
        public async Task<AuthServiceResponse> LoginAsync(Login login)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Register
        public Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ValidateMobile
        public async Task<Response> ValidateMobile(ValidateMobile validateMobile, string otp)
        {
        return await _authRepository.ValidateMobile(validateMobile,otp);
        }
        #endregion
    }
}
