using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.AuthInterfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration,List<string>roleId);
        Task<Token> LoginAsync(Login login);
        Task<AuthServiceResponse> AddDynamicRole(Role role);
        Task<List<Role>> GetRoles();
        Task<Response> ValidateMobile(ValidateMobile validateMobile);
        Task<Token> GetRefreshToken(GetRefreshToken getRefreshToken);
        Task<AuthServiceResponse>CreateSOPin(CreateSOPin createSOPin,string soID);

    }
}
