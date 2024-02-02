
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessageHolder<TParticipant, TBaseMessage, TBasePinnedMessage, TInvite, TMute> : BaseEntity<ulong>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ICollection<TBaseMessage> Messages { get; set; }
        public ICollection<TBasePinnedMessage> PinnedMessages { get; set; }
        public ICollection<TParticipant> Participants { get; set; }
        public ICollection<TMute> Mutes { get; set; }
        public ICollection<TInvite> Invites { get; set; }
    }
}
