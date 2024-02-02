using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore;
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

        public ICollection<AccountBlock>? BlockedAccounts { get; set; } //This account blocks other accounts through this
        public ICollection<AccountNote>? NotedAccounts { get; set; } //This account adds notes about other accounts
        public ICollection<AccountMute>? MutedVoices { get; set; } //This account adds mutes for other accounts voice
        public ICollection<AccountSoundboardMute>? MutedSoundboards { get; set; } //This account adds mutes for other accounts soundboard
        public ICollection<IncomingFriendRequest>? IncomingFriendRequests { get; set; }
        public ICollection<OutgoingFriendRequest>? OutgoingFriendRequests { get; set; }
        public ICollection<ServerTextChannelMessageReport>? ChannelMessageReports { get; set; }
        public ICollection<AccountProfileReport>? AccountProfileReports { get; set; }
        public ICollection<ChatMessageReport>? ChatMessageReports { get; set; }



        public ICollection<GroupChat>? Chats { get; set; } //mapped through chatparticipant
        public ICollection<Server>? Server { get; set; } //mapped through serverprofile

        public ICollection<Friendship>? Friendships { get; set; } //mapped through friendshipparticipant
        public ICollection<FriendSuggestion>? FriendSuggestions { get; set; } //mapped through friendshipparticipant
    }
}
