namespace Echo.Application.Contracts.DTO.Contracts;

public interface ISentMessage<TSender> : IMessage
{
    TSender? Sender { get; set; }
}
