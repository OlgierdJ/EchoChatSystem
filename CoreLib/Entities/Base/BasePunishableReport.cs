using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BasePunishableReport<TId, TReporter, TReporterId, TReason, TReasonId, TSubject, TSubjectId, TPunishment, TPunishmentId> : BaseReport<TId, TReporter, TReporterId, TReason, TReasonId, TSubject, TSubjectId> where TReason : BaseReportReason<TReasonId>
    {
        public TPunishmentId? ViolationId { get; set; }
        public TPunishment? Violation { get; set; }
    }
}
