using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ReportCore;
using CoreLib.Entities.EchoCore.ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class Account : BaseEntity<ulong>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeLastLogon { get; set; }
        public string? UserId { get; set; }
        public bool FocusModeEnabled { get; set; } //for not receiving in app sounds
        public byte? ActivityStatusId { get; set; }

        public AccountActivityStatus? ActivityStatus { get; set; }

        public AccountProfile Profile { get; set; } //mapped through connections?

        public ICollection<AccountConnection>?  Connections { get; set; }

        //Interactivity stuff
        public ICollection<AccountBlock>? BlockedAccounts { get; set; } //This account blocks other accounts through this
        public ICollection<AccountNote>? NotedAccounts { get; set; } //This account adds notes about other accounts
        public ICollection<AccountMute>? MutedVoices { get; set; } //This account adds mutes for other accounts voice
        public ICollection<ChatMute>? MutedChats { get; set; } //This account adds mutes for other accounts voice
        public ICollection<AccountSoundboardMute>? MutedSoundboards { get; set; } //This account adds mutes for other accounts soundboard

        //report stuff
        public ICollection<ReportedAccountProfile>? ReportedAccountProfiles { get; set; }
        public ICollection<AccountProfileReport>? AccountProfileReports { get; set; }
        public ICollection<ReportedMessage>? ReportedMessages { get; set; } //reported messages that are owned by this account
        public ICollection<MessageReport>? MessageReports { get; set; } //reports sent by this account about other accounts messages
        
        //friend stuff //works
        public ICollection<IncomingFriendRequest>? IncomingFriendRequests { get; set; }
        public ICollection<OutgoingFriendRequest>? OutgoingFriendRequests { get; set; }
        public ICollection<Friendship>? Friendships { get; set; } //mapped through friendshipparticipant
        public ICollection<FriendSuggestion>? FriendSuggestions { get; set; } //mapped through connections?

        //chat stuff
        public ICollection<Chat>? Chats { get; set; } //mapped through chatparticipancy
        public ICollection<ChatInvite>? ChatInvites { get; set; }
        public ICollection<ChatMessage>?  ChatMessages { get; set; }

        //Server stuff
        public ICollection<ServerProfile>? Servers { get; set; } //mapped through serverprofile
        public ICollection<ServerInvite>?  ServerInvites { get; set; }
        public ICollection<ServerTextChannelMessage>?  ChannelMessages { get; set; }




    }
}
