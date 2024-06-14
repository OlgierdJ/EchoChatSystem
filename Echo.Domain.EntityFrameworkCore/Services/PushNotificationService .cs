using Echo.Domain.Shared.Enums;
using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Interfaces.Services;

namespace Echo.Domain.EntityFrameworkCore.Services;

public class PushNotificationService : IPushNotificationService
{
    private Dictionary<string, Func<IEntity, EntityAction, Task>> _hubManager = new();
    

    public async Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity
    {
        await Task.Run(async () =>
        {
            var task = _hubManager[typeof(T).Name];
            await task(entity, action);
        });
    }

    public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity
    {
        throw new NotImplementedException();
    }
}
