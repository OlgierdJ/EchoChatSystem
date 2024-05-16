namespace CoreLib.Entities.Base
{
    public abstract class BaseMessageAttachment<TId, TMessage, TMessageId> : BaseEntity<TId>
    {
        public TMessageId MessageId { get; set; }
        //public string AttachmentType { get; set; }
        public string FileURL { get; set; }

        public TMessage Message { get; set; }
    }
}
