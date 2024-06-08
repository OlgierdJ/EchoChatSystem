using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Feedback
{
    public class FeedbackReportReason : BaseEntity<byte>, IReportReason<FeedbackReport>
    {
        public ICollection<FeedbackReport>? Reports { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}