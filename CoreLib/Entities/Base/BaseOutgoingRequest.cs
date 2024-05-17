namespace CoreLib.Entities.Base
{
    public class BaseOutgoingRequest<TId, TSender, TSenderId, TReceiverRequest> : BaseEntity<TId>
    {
        public TSenderId SenderId { get; set; }

        public TSender Sender { get; set; }
        public TReceiverRequest ReceiverRequest { get; set; }
    }
}
