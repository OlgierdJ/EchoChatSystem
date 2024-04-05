using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces
{
    public interface IPushNotificationService
    {
        public Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity;
        public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity;
    }
}
