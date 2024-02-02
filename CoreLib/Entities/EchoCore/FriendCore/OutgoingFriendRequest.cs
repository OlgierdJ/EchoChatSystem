﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class OutgoingFriendRequest : BaseEntity<ulong>
    {
        public string SenderId { get; set; }

        public Account Sender { get; set; }
        public IncomingFriendRequest ReceiverRequest { get; set; }
    }
}