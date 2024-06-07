namespace CoreLib.Entities.Base
{
    public interface IMessagePin<TMessage, TMessageId, TPinboard, TPinboardId>
    {
        public TPinboardId PinboardId { get; set; }
        public TMessageId MessageId { get; set; }

        public TPinboard Pinboard { get; set; }
        public TMessage Message { get; set; }
    }
}
