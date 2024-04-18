using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.Bug;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountViolation : BaseViolation<ulong, Account, ulong, Account, ulong>
    {
        public ICollection<CustomStatusReport> ConsumedCustomStatusReports { get; set; }
        public ICollection<MessageReport> ConsumedMessageReports { get; set; }
        public ICollection<ProfileReport> ConsumedProfileReports { get; set; }
    }
}
