using Echo.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Echo.Domain.EntityFrameworkCore.DomainEvents;

public class PreDomainEvent
{
    public string Type { get; set; } //need this for conversion probably
    public EntityEntry Entry { get; set; }
    public EntityAction Action { get; set; }
}
