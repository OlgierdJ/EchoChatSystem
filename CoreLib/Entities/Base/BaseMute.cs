using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMute<TMuter, TMuterId, TSubject, TSubjectId> : BaseEntity<ulong>
    {
        public TMuter Muter { get; set; }
        public TSubject Subject { get; set; }
        public TSubjectId SubjectId { get; set; }
        public TMuterId MuterId { get; set; }
        public DateTime TimeMuted { get; set; } //null = permanent
        public DateTime? ExpirationTime { get; set; } //null = permanent
    }
}
