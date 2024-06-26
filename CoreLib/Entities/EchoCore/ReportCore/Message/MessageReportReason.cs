﻿using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Message
{
    public class MessageReportReason : BaseEntity<byte>, IReportReason<MessageReport>
    {
        public ICollection<MessageReport>? Reports { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
