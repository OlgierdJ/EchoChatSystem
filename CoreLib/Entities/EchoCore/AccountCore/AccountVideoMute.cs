using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountVideoMute : BaseMute<Account, ulong, Account, ulong> //mutes the video stream of the other account thus ignoring it
    {
    }
}
