namespace CoreLib.DTO.Contracts;

public interface IRepliableMessage<TReply> : IMessage
{
    public TReply? Replied { get; set; } //message parent if present.
}
