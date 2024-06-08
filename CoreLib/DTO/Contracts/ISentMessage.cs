namespace CoreLib.DTO.Contracts
{
    public interface ISentMessage<TSender> : IMessage
    {
        TSender? Sender { get; set; }
    }
}
