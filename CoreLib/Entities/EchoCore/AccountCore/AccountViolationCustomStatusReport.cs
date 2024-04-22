﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountViolationCustomStatusReport : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong CustomStatusReportId { get; set; }
        public Account Account { get; set; }
        public CustomStatusReport CustomStatusReport { get; set; }
    }
}