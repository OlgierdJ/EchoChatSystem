using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore
{
    public class ServerDeafen : BaseMute<Server, ulong, Account, ulong> //serverwide deafen overrides user deafen setting.
    {
    }
}
