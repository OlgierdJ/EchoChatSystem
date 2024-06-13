namespace Echo.Application.Contracts.DTO.Contracts;

public interface IMessage
{

    string Content { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    DateTime? TimeEdited { get; set; }
    DateTime TimeSent { get; set; }
}
