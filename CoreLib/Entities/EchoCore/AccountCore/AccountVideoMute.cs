using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountVideoMute : BaseMute<Account, ulong, Account, ulong> //mutes the video stream of the other account thus ignoring it
    {
    }
}
