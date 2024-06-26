﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ChatCore;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class DirectMessageRelation : BaseEntity<ulong> //one of these will be created each time a user tries to message another user for the first time and the chat will be saved permanently
    {
       public ICollection<AccountDirectMessageRelation> AccountsInRelation { get; set; }
       public DateTime TimeCreated { get; set; }
        public ulong ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
