using CoreLib.Entities.EchoCore.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public class BaseMessageHolder<TParticipant, TBaseMessage, TBasePinnedMessage, TInvites, TMute> : BaseEntity<ulong>
    {
        public ICollection<TBaseMessage> Messages { get; set; }
        public ICollection<TBasePinnedMessage> PinnedMessages { get; set; }
        public ICollection<TParticipant> Participants { get; set; }
        public ICollection<TMute> Mutes { get; set; }
        public ICollection<TInvites> Invites { get; set; }
    }
}
