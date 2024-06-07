namespace CoreLib.Entities.Base
{
    public interface IMessageAttachment<TMessage, TMessageId>
    {
        public TMessageId MessageId { get; set; }
        //public string AttachmentType { get; set; }
        public string FileLocationURL { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }

        public TMessage Message { get; set; }
    }
}
