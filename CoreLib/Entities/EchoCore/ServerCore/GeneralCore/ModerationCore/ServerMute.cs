using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore
{
    public class ServerMute : BaseMute<Server, ulong, Account, ulong> //serverwide mute overrides accountmute relation
    {
    }
}
