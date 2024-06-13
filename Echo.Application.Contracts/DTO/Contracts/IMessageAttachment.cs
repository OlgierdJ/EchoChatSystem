namespace Echo.Application.Contracts.DTO.Contracts;

public interface IMessageAttachment
{
    string? Description { get; set; }
    string FileLocationURL { get; set; }
    string FileName { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
}