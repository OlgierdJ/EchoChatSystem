using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountNicknameDTO //: BaseNickname<ulong, Account, ulong, Account, ulong>
    {
        public ulong SubjectId { get; set; }
        public string Nickname { get; set; }
    }
}
