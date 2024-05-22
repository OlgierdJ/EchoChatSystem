namespace CoreLib.Entities.Base
{
    public abstract class BaseMessageAttachment<TId, TMessage, TMessageId> : BaseEntity<TId>
    {
        public TMessageId MessageId { get; set; }
        //public string AttachmentType { get; set; }
        public string FileLocationURL { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }

        public TMessage Message { get; set; }
    }
}
