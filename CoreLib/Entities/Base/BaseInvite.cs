using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public class BaseInvite<TInviter, TInviterId, TSubject,TSubjectId> : BaseEntity<ulong>
    {
        public TSubjectId ChatId { get; set; }
        public TInviterId InviterId { get; set; }
        public string InviteCode { get; set; }
        public DateTime? ExpirationTime { get; set; } // null = permanent
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; } //0 means unlimited

        public TInviter Inviter { get; set; }
        public TSubject Chat { get; set; }
    }
}
