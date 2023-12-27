using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.AuthModels
{
    public class GetRefreshToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
