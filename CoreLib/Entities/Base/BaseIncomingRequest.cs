namespace CoreLib.Entities.Base
{
    public abstract class BaseIncomingRequest<TId, TReceiver, TReceiverId, TSenderRequest> : BaseEntity<TId> //mayb review
    {
        public TId SenderRequestId { get; set; }
        public TReceiverId ReceiverId { get; set; }
        public TReceiver Receiver { get; set; }
        public TSenderRequest SenderRequest { get; set; }
    }
}
