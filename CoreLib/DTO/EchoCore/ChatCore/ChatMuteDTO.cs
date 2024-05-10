using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    //ved ik helt om det noget så bliver lave men..
    public class ChatMuteDTO //: BaseMute<ulong, Account, ulong, Chat, ulong>
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; } //null = permanent
    }
}
