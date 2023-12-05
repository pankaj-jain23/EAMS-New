using EAMS.Helper;
using EAMS_ACore.AuthModels;
using EAMS_ACore.HelperModels;
using EAMS_ACore.Models;
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
        Task<SectorOfficerMaster> ValidateMobile(ValidateMobile validateMobile);

        Task<AuthServiceResponse> SectorOfficerMasterRecord(SectorOfficerMaster sectorOfficerMaster);
        Task<AuthServiceResponse> FindUserByName(UserRegistration userRegistration);
        Task<UserRegistration> CheckUserLogin(Login login);
        Task<AuthServiceResponse> CreateUser(UserRegistration userRegistration, List<string> roleId);
        Task<AuthServiceResponse> UpdateUser(UserRegistration userRegistration);
        Task<List<Role>> GetRoleByUser(Login login);
        Task<AuthServiceResponse> CreateSOPin(CreateSOPin createSOPin,string soId);

        Task<SectorOfficerMaster>GetSOById(int soId);

    }
}
