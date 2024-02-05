using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseInvite<TId> : BaseEntity<TId>
    {
        public string InviteCode { get; set; }
        public DateTime? ExpirationTime { get; set; } // null = permanent
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; } //0 means unlimited
    }
    public abstract class BaseInvite<TId, TInviter, TInviterId, TSubject, TSubjectId> : BaseInvite<TId>
    {
        public TSubjectId SubjectId { get; set; }
        public TInviterId InviterId { get; set; }

        public TInviter Inviter { get; set; }
        public TSubject Subject { get; set; }
    }

}
