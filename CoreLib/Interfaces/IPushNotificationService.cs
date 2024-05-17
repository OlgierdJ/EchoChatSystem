using CoreLib.Entities.Enums;

namespace CoreLib.Interfaces
{
    public interface IPushNotificationService
    {
        public Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity;
        public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity;
    }
}
