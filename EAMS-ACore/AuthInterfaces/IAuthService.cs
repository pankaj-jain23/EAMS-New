using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.AuthInterfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse> RegisterAsync(UserRegistration userRegistration,List<string>roleId, List<string> stateIds, List<string> districtIds, List<string> pcIds, List<string> assemblyIds);
        Task<Token> LoginAsync(Login login);
        Task<ServiceResponse> AddDynamicRole(Role role);
        Task<List<Role>> GetRoles();
        Task<Response> ValidateMobile(ValidateMobile validateMobile);
        Task<Token> GetRefreshToken(GetRefreshToken getRefreshToken);
        Task<ServiceResponse>CreateSOPin(CreateSOPin createSOPin,string soID);
        Task<UserList> GetDashboardProfile(string userId);




    }
}
