using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseEntityTracker<TId, TOwner, TOwnerId, TSubject, TSubjectId> : BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; } //owner entity (cascade)
        public TSubjectId? SubjectId { get; set; } //tracked entity

        public TOwner Owner { get; set; }
        public TSubject? Subject { get; set; }
    }
    public abstract class BaseEntityTracker<TId, TOwner, TOwnerId, TSubject, TSubjectId, THolder, THolderId> : BaseEntityTracker<TId, TOwner, TOwnerId, TSubject, TSubjectId>
    {
        public THolderId HolderId { get; set; } //entity that holds the tracker (cascade)
        public THolder Holder { get; set; }
    }
}
