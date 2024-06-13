namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IAuditableMessage : IMessage
{
    public DateTime? TimeEdited { get; set; }
}
