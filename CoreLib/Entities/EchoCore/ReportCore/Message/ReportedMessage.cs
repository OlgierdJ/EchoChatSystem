﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ReportCore.Message
{
    public class ReportedMessage //: BaseMessage<ulong, Account, ulong>
    //when a message is reported the unencrypted content of the message, timestamp, and the attachments are to be duplicated into one of these.
    //and saved with their own encryption that support-people has access to.
    {
        //public DateTime TimeReported { get; set; }
        public string MessageType { get; set; } //idk something like Chat/DM, TextChannel, Other
        public ICollection<ReportedMessageAttachment>? Attachments { get; set; }

        public MessageReport Report { get; set; }
    }
}
