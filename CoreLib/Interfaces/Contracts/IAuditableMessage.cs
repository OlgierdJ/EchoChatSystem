namespace CoreLib.Interfaces.Contracts;

public interface IAuditableMessage : IMessage
{
    public DateTime? TimeEdited { get; set; }
}
