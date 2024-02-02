using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseReport : BaseEntity<ulong>
    {
        public string Message { get; set; }
        public DateTime TimeAdded { get; set; }
    }
    public abstract class BaseReport<TReporterEntity, TReporterEntityId> : BaseReport
    {
        public TReporterEntityId ReporterId { get; set; }
        public TReporterEntity? Reporter { get; set; }
    }

    public abstract class BaseReport<TReporterEntity, TReporterEntityId, TSubjectEntity, TSubjectEntityId, TReasonEntity> : BaseReport<TReporterEntity, TReporterEntityId> where TReasonEntity : BaseReportReason
    {
        public IEnumerable<TReasonEntity>? Reasons { get; set; }
        public TSubjectEntityId SubjectId { get; set; }
        public TSubjectEntity? Subject { get; set; }
    }
}
