﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ReportCore.Message
{
    public class MessageReport : BaseReport<ulong, Account, ulong, MessageReportReason, byte, ReportedMessage, ulong>
    {
        public ICollection<MessageReportReason> Reasons { get; set; }
    }
}