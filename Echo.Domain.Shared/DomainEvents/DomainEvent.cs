using Echo.Domain.Shared.Enums;

namespace Echo.Domain.Shared.DomainEvents;

public class DomainEvent
{
    public string Type { get; set; } //need this for conversion probably
    public string Entity { get; set; }
    public EntityAction Action { get; set; }
}
