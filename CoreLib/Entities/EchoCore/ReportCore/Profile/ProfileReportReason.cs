using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Profile
{
    public class ProfileReportReason : BaseEntity<byte>, IReportReason<ProfileReport>
    {
        public ICollection<ProfileReport>? Reports { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
