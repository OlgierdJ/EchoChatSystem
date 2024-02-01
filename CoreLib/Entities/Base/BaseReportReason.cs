using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseReportReason : BaseEntity<int>
    {
        public string Reason { get; set; }
        public string Description { get; set; }
    }

    public abstract class BaseReportReason<TReportEntity> : BaseReportReason /*where TReportToReasonEntity : BaseReportReportReason*/
    {
        public IEnumerable<TReportEntity>? Reports { get; set; }
    }
}
