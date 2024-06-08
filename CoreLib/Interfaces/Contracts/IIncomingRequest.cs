namespace CoreLib.Interfaces.Contracts
{
    public interface IIncomingRequest<TId, TReceiver, TReceiverId, TSenderRequest>
    {
        public TId SenderRequestId { get; set; }
        public TReceiverId ReceiverId { get; set; }
        public TReceiver Receiver { get; set; }
        public TSenderRequest SenderRequest { get; set; }
    }
}
