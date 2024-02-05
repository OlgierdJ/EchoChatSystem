using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMute<TId, TMuter, TMuterId, TSubject, TSubjectId> : BaseEntity<TId>
    {
        public TSubjectId SubjectId { get; set; }
        public TMuterId MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; } //null = permanent
        public TMuter Muter { get; set; }
        public TSubject Subject { get; set; }
    }
}
