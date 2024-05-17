namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public class MessageAttachmentDTO
    {
        public ulong Id { get; set; } //id of original attachment relative to the message.
        public string FileName { get; set; } //name of file name can be spoiler tagged SPOILER_name should tell echo to display as spoiler.
        public string FileLocationURL { get; set; } //relative site where file is stored by filename
        public string? Description { get; set; } //alt text for text readers
    }
}