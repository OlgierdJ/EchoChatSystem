using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DomainCoreApi.EFCORE.Interceptors;


public class PreDomainEvent
{
    public string Type { get; set; } //need this for conversion probably
    public EntityEntry Entry { get; set; }
    public EntityAction Action { get; set; }
}
