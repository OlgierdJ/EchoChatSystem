﻿using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Bug
{
    public class BugReportReason :BaseEntity<byte>, IReportReason<BugReport>
    {
        public ICollection<BugReport>? Reports { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
