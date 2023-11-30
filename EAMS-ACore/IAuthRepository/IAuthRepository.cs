using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IAuthRepository
{
    public interface IAuthRepository
    {
        Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration);
        Task<AuthServiceResponse> LoginAsync(Login login);

        Task<AuthServiceResponse> AddDynamicRole(Role role);
        Task<List<Role>> GetRoles();
        Task<Response> ValidateMobile(ValidateMobile validateMobile, string otp);
    }
}
