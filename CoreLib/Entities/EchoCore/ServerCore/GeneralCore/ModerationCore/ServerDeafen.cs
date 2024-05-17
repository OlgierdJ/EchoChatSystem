using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore
{
    public class ServerDeafen : BaseMute<Server, ulong, Account, ulong> //serverwide deafen overrides user deafen setting.
    {
    }
}
