using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public class BaseViolation<TId, TIssuer, TIssuerId, TSubject, TSubjectId> : BaseEntity<TId>
    {
        public TSubjectId SubjectId { get; set; }
        public TIssuerId IssuerId { get; set; }
        public int Severity { get; set; } //1 - 5 how bad the violation is (this affects how much impact the violation has on the standing of the affected subject)
        public DateTime ExpirationDate { get; set; } //When the violation no longer affects the standing of the affected subject subject
        public TSubject Subject { get; set; }
        public TIssuer  Issuer { get; set; }
    }
}
