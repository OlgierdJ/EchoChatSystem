using CoreLib.Entities.Enums;

namespace CoreLib.Hubs;

public class DomainEvent
{
    public string Type { get; set; } //need this for conversion probably
    public string Entity { get; set; }
    public EntityAction Action { get; set; }
}
