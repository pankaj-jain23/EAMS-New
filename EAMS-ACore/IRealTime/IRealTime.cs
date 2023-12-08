using EAMS_ACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.IRealTime
{
    public interface IRealTime
    {
        Task<DashBoardRealTimeCount> SendDashBoardCount();
    }
}
