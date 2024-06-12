namespace CoreLib.DTO.RequestCore.MessageCore;

public class SendAttachmentRequestDTO //sent to message controller or within a message to create attachments 
{
    public string FileName { get; set; } //name of file name can be spoiler tagged SPOILER_name should tell echo to display as spoiler.
    public string FileLocationURL { get; set; } //relative site where file is stored by filename
    public string? Description { get; set; } //alt text for text readers
}