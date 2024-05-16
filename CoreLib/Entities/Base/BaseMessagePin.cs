namespace CoreLib.Entities.Base
{
    public abstract class BaseMessagePin<TMessage, TMessageId, TPinboard, TPinboardId>
    {
        public TPinboardId PinboardId { get; set; }
        public TMessageId MessageId { get; set; }

        public TPinboard Pinboard { get; set; }
        public TMessage Message { get; set; }
    }
}
