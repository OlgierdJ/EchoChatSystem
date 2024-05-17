using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountViolation : BaseViolation<ulong, Account, ulong, Account, ulong, AccountViolationAppeal>
    {
        public ICollection<CustomStatusReport> ConsumedCustomStatusReports { get; set; }
        public ICollection<MessageReport> ConsumedMessageReports { get; set; }
        public ICollection<ProfileReport> ConsumedProfileReports { get; set; }
    }
}
