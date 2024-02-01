using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ChatCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class Account : BaseEntity<string>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastLogonDate { get; set; }
        public string? UserId { get; set; }
        public ICollection<AccountBlock>? BlockedAccounts { get; set; }
        public ICollection<Friendship>? Friendships { get; set; } //mapped through friendshipparticipant
        public ICollection<IncomingFriendRequest>? IncomingFriendRequests { get; set; }
        public ICollection<OutgoingFriendRequest>? OutgoingFriendRequests { get; set; }
        public ICollection<Chat>? Chats { get; set; } //mapped through chatparticipant
    }
}
