using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountViolationMessageReport : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong MessageReportId { get; set; }
        public Account Account { get; set; }
        public MessageReport MessageReport { get; set; }
    }
}
