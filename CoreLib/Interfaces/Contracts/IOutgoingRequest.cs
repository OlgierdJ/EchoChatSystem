namespace CoreLib.Interfaces.Contracts
{
    public interface IOutgoingRequest<TSender, TSenderId, TReceiverRequest>
    {
        public TSenderId SenderId { get; set; }

        public TSender Sender { get; set; }
        public TReceiverRequest ReceiverRequest { get; set; }
    }
}
