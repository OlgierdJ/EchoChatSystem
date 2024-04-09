using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ReportCore.CustomStatus
{
    public class CustomStatusReport : BaseReport<ulong, Account, ulong, CustomStatusReportReason, byte, ReportedCustomStatus, ulong>
    {
    }
}
