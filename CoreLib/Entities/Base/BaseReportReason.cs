using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseReportReason<TId> : BaseEntity<TId>
    {
        public string Reason { get; set; }
        public string Description { get; set; }
    }

    public abstract class BaseReportReason<TId,TReport> : BaseReportReason<TId> /*where TReportToReasonEntity : BaseReportReportReason*/
    {
        public ICollection<TReport>? Reports { get; set; }
    }
}
