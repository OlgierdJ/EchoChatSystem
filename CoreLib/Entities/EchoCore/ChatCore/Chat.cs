using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.FriendCore;
using System.Threading;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class Chat : BaseEntity<ulong>, IBaseMessageHolder<ulong, ChatMessage, ChatParticipancy, ChatInvite, ChatMute>, IInviteHolder<ChatInvite>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        //public TPinboard? Pinboard { get; set; }
        public ICollection<ChatMute>? Mutes { get; set; }

        public ICollection<ChatInvite>? Invites { get; set; }

        public ICollection<ChatParticipancy>? Participants { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
        public DirectMessageRelation? DirectMessageRelation { get; set; } //if chat is part of friendship then you cant add participants or leave it.
        public string? IconUrl { get; set; }
        //make sure the participants know that there is an ongoing call or video call in the chat dont know how variable? live data? how dafuq
        public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
        public ICollection<ChatMessagePin>? PinnedMessages { get; set; }
    }
}
