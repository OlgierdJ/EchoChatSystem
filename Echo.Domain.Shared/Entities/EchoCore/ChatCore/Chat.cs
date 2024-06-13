using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ChatCore;

public class Chat : BaseEntity<ulong>,
    IMessageHolder<ChatMessage>,
    IParticipable<ChatParticipancy>,
    IInviteHolder<ChatInvite>,
    IMutable<ChatMute>,
    IPinboard<ChatMessagePin>
{
    public string Name { get; set; }
    public DateTime TimeCreated { get; set; }
    public ICollection<ChatMute>? Muters { get; set; }

    public ICollection<ChatInvite>? Invites { get; set; }

    //make sure the participants know that there is an ongoing call
    //or video call in the chat dont know how variable? live data? how dafuq
    public ICollection<ChatParticipancy>? Participants { get; set; }
    public ICollection<ChatMessage>? Messages { get; set; }
    //if chat is part of friendship then you cant add participants or leave it.
    public DirectMessageRelation? DirectMessageRelation { get; set; }
    public string? IconUrl { get; set; }
    public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
    public ICollection<ChatMessagePin>? PinnedMessages { get; set; }
}
