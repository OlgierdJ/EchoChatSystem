using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.Management;
using CoreLib.Entities.EchoCore.UserCore;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class Account : BaseEntity<ulong>
    {
        //core properties which are public to other app users
        public string Name { get; set; } //unique name handle used by others to add you
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeLastLogon { get; set; } //maybe add table to map sessions with login times, logoff times, devices etc to allow the user to disconnect unused devices and see their device history
        public ulong? UserId { get; set; } //used for mapping personal information to the account (real name address password loginusername etc)
        public byte ActivityStatusId { get; set; }
        //public ulong? CustomStatusId { get; set; } //pk to pk dependent

        public User? User { get; set; }
        public AccountActivityStatus ActivityStatus { get; set; }
        public AccountCustomStatus? CustomStatus { get; set; }
        public AccountProfile Profile { get; set; } //should be created on account creation

        //private settings and information about this account only used by the user and the application / api
        public ICollection<AccountConnection>? Connections { get; set; }
        public AccountSettings Settings { get; set; } //should be created on account creation

        //Interactivity stuff - also private settings and information about this account only used by the user and the application / api
        //public ICollection<AccountBadge>? Badges { get; set; } //some are only owned by account, removed by external factors such as no longer subscriber.
        public ICollection<AccountRole>? Roles { get; set; } //System / Application roles (perhaps not needed)
        public ICollection<AccountViolation>? Violations { get; set; } //Violations affecting system functions depending on total severity of nonexpired violations
        public ICollection<AccountViolation>? IssuedViolations { get; set; } //Violations affecting system functions depending on total severity of nonexpired violations
        public ICollection<AccountViolationAppealReview>? ReviewedAppeals { get; set; } //Violations affecting system functions depending on total severity of nonexpired violations
        public ICollection<AccountSession>? Sessions { get; set; } //Sessions for the account logging the device, location from which the session is valid and also when the validity expires, and alternatively allows the user to revoke validity of a session
        public ICollection<AccountBlock>? BlockedAccounts { get; set; } //This account blocks other accounts through this
        public ICollection<AccountNickname>? NicknamedAccounts { get; set; } //This account adds notes about other accounts
        public ICollection<AccountAccountVolume>? PersonalAccountVolumes { get; set; } //This account adds notes about other accounts
        public ICollection<AccountDirectMessageRelation>? DirectMessageRelations { get; set; } //This account adds notes about other accounts
        //public ICollection<AccountAccountVolume>? PersonalAccountVolumes { get; set; } //This account adds notes about other accounts
        public ICollection<AccountNote>? NotedAccounts { get; set; } //This account adds notes about other accounts
        public ICollection<AccountMute>? MutedVoices { get; set; } //This account adds mutes for other accounts voice
        public ICollection<ChatMute>? MutedChats { get; set; } //This account adds mutes for other accounts voice
        public ICollection<AccountServerMute>? MutedServers { get; set; } //This account adds mutes for other Servers
        public ICollection<ServerTextChannelMute>? MutedTextChannels { get; set; } //This account adds mutes for other ServerChannel
        public ICollection<AccountServerVoiceChannelMute>? MutedVoiceChannels { get; set; } //This account adds mutes for other ServerChannel
        public ICollection<AccountSoundboardMute>? MutedSoundboards { get; set; } //This account adds mutes for other accounts soundboard
        public ICollection<ChatAccountMessageTracker>? ChatMessageTrackers { get; set; }
        public ICollection<ServerTextChannelAccountMessageTracker>? TextChannelMessageTrackers { get; set; }

        //report stuff - also private settings and information about this account only used by the user and the application / api
        public ICollection<ReportedCustomStatus>? ReportedCustomStatuses { get; set; } //reported customstatuses that are owned by this account
        public ICollection<CustomStatusReport>? CustomStatusReports { get; set; } //reports sent by this account about other customstatuses
        public ICollection<ReportedProfile>? ReportedProfiles { get; set; } //reported accountprofiles that are owned by this account
        public ICollection<ProfileReport>? ProfileReports { get; set; } //reports sent by this account about other accountprofiles
        public ICollection<ReportedMessage>? ReportedMessages { get; set; } //reported messages that are owned by this account
        public ICollection<MessageReport>? MessageReports { get; set; } //reports sent by this account about other accounts messages

        //friend stuff - also private settings and information about this account only used by the user and the application / api
        public ICollection<IncomingFriendRequest>? IncomingFriendRequests { get; set; }
        public ICollection<OutgoingFriendRequest>? OutgoingFriendRequests { get; set; }
        public ICollection<FriendshipParticipancy>? Friendships { get; set; } //mapped through friendshipparticipant
        public ICollection<FriendSuggestion>? FriendSuggestions { get; set; } //mapped through connections?

        //chat stuff - public to other members of the chat
        public ICollection<ChatParticipancy>? Chats { get; set; } //mapped through chatparticipancy
        public ICollection<ChatInvite>? ChatInvites { get; set; } //done
        public ICollection<ChatMessage>? ChatMessages { get; set; }

        //Server stuff - public to other members of the server
        public ICollection<ServerProfile>? Servers { get; set; } //mapped through serverprofile //NOTE COMMENTED OUT TEMPORARILY
        public ICollection<ServerInvite>? ServerInvites { get; set; }
        public ICollection<ServerEvent>? ServerEvents { get; set; }
        public ICollection<AccountServerFolder>? Folders { get; set; }
        public ICollection<ServerTextChannelMessage>? ChannelMessages { get; set; }

    }
}
