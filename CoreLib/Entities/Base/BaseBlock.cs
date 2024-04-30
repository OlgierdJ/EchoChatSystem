using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseBlock<TBlockerEntity, TBlockerEntityId, TBlockedEntity, TBlockedEntityId> //done
    {
        public TBlockerEntityId BlockerId { get; set; }
        public TBlockedEntityId BlockedId { get; set; }
        public DateTime TimeBlocked { get; set; }
        public TBlockerEntity  Blocker { get; set; }
        public TBlockedEntity  Blocked { get; set; }
    }
}
