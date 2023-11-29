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
        Task<AuthServiceResponse> RegisterAsync(UserRegistration userRegistration );
        Task<AuthServiceResponse> LoginAsync(Login login );
    }
}
