using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMessage : BaseEntity<ulong>, IAuditableMessage, IAuthoredEntity<Account, ulong?>, IRepliableMessageEntity<ServerTextChannelMessage, ulong?>
{
    public ICollection<ServerTextChannelMessageAttachment>? Attachments { get; set; }
    public ServerTextChannelMessagePin? MessagePin { get; set; }
    public ICollection<AccountServerTextChannelMessageTracker>? MessageTrackers { get; set; }
    public ulong MessageHolderId { get; set; }
    public ServerTextChannel? MessageHolder { get; set; }
    public ulong? AuthorId { get; set; }
    public Account? Author { get; set; }
    public string Content { get; set; }
    public DateTime TimeSent { get; set; }
    public DateTime? TimeEdited { get; set; }
    public ulong? ParentId { get; set; }
    public ServerTextChannelMessage? Parent { get; set; }
    public ICollection<ServerTextChannelMessage>? Children { get; set; }
}
