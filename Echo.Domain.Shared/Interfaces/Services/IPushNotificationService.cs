using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Enums;

namespace Echo.Domain.Shared.Interfaces.Services;

public interface IPushNotificationService
{
    public Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity;
    public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity;
}
