namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface IMessageAttachment
    {
        string? Description { get; set; }
        string FileLocationURL { get; set; }
        string FileName { get; set; }
        ulong Id { get; set; }
    }

    public class MessageAttachmentDTO : IMessageAttachment
    {
        public ulong Id { get; set; } //id of original attachment relative to the message.
        public string FileName { get; set; } //name of file name can be spoiler tagged SPOILER_name should tell echo to display as spoiler.
        public string FileLocationURL { get; set; } //relative site where file is stored by filename
        public string? Description { get; set; } //alt text for text readers
    }
}