using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
 
    public class AccountViolationProfileReport : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong ProfileReportId { get; set; }
        public Account Account { get; set; }
        public ProfileReport ProfileReport { get; set; }
    }
}
