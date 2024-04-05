using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.EchoCore;
using CoreLib.Interfaces;
using Microsoft.AspNetCore.SignalR;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        //protected readonly IHubContext<ClientHub, IClientHubClient> _hubContext;
        private Dictionary<string, Func<IEntity, EntityAction, Task>> _hubManager = new();
        //public PushNotificationService(IHubContext<ClientHub, IClientHubClient> hubContext)
        //{
        //    _hubContext = hubContext;
        //    _hubManager.Add(nameof(Account), async (entity, action) => { await _hubContext.Clients.All.ReceiveAccountUpdateMessage((Account)entity, action); });
        //    _hubManager.Add(nameof(AccountSettings), async (entity, action) => { await _hubContext.Clients.All.ReceiveAccountSettingsUpdateMessage((AccountSettings)entity, action); });
        //    _hubManager.Add(nameof(CategoryImage), async (entity, action) => { await _hubContext.Clients.All.ReceiveCategoryImageUpdateMessage((CategoryImage)entity, action); });
        //    _hubManager.Add(nameof(Category), async (entity, action) => { await _hubContext.Clients.All.ReceiveCategoryUpdateMessage((Category)entity, action); });
        //    _hubManager.Add(nameof(CurrentSession), async (entity, action) => { await _hubContext.Clients.All.ReceiveCurrentSessionUpdateMessage((CurrentSession)entity, action); });
        //    _hubManager.Add(nameof(Icon), async (entity, action) => { await _hubContext.Clients.All.ReceiveIconUpdateMessage((Icon)entity, action); });
        //    _hubManager.Add(nameof(IconTemplate), async (entity, action) => { await _hubContext.Clients.All.ReceiveIconTemplateUpdateMessage((IconTemplate)entity, action); });
        //    _hubManager.Add(nameof(ItemComment), async (entity, action) => { await _hubContext.Clients.All.ReceiveItemCommentUpdateMessage((ItemComment)entity, action); });
        //    _hubManager.Add(nameof(ItemOrderHistory), async (entity, action) => { await _hubContext.Clients.All.ReceiveItemOrderHistoryUpdateMessage((ItemOrderHistory)entity, action); });
        //    _hubManager.Add(nameof(Item), async (entity, action) => { await _hubContext.Clients.All.ReceiveItemUpdateMessage((Item)entity, action); });
        //    _hubManager.Add(nameof(Location), async (entity, action) => { await _hubContext.Clients.All.ReceiveLocationUpdateMessage((Location)entity, action); });
        //    _hubManager.Add(nameof(LoginAttempt), async (entity, action) => { await _hubContext.Clients.All.ReceiveLoginAttemptUpdateMessage((LoginAttempt)entity, action); });
        //    _hubManager.Add(nameof(ModelDetail), async (entity, action) => { await _hubContext.Clients.All.ReceiveModelDetailUpdateMessage((ModelDetail)entity, action); });
        //    _hubManager.Add(nameof(ModelImage), async (entity, action) => { await _hubContext.Clients.All.ReceiveModelImageUpdateMessage((ModelImage)entity, action); });
        //    _hubManager.Add(nameof(Model), async (entity, action) => { await _hubContext.Clients.All.ReceiveModelUpdateMessage((Model)entity, action); });
        //    _hubManager.Add(nameof(OrderComment), async (entity, action) => { await _hubContext.Clients.All.ReceiveOrderCommentUpdateMessage((OrderComment)entity, action); });
        //    _hubManager.Add(nameof(OrderDetailComment), async (entity, action) => { await _hubContext.Clients.All.ReceiveOrderDetailCommentUpdateMessage((OrderDetailComment)entity, action); });
        //    _hubManager.Add(nameof(OrderDetail), async (entity, action) => { await _hubContext.Clients.All.ReceiveOrderDetailUpdateMessage((OrderDetail)entity, action); });
        //    _hubManager.Add(nameof(Order), async (entity, action) => { await _hubContext.Clients.All.ReceiveOrderUpdateMessage((Order)entity, action); });
        //    _hubManager.Add(nameof(OrderStatus), async (entity, action) => { await _hubContext.Clients.All.ReceiveOrderStatusUpdateMessage((OrderStatus)entity, action); });
        //    _hubManager.Add(nameof(Role), async (entity, action) => { await _hubContext.Clients.All.ReceiveRoleUpdateMessage((Role)entity, action); });
        //    _hubManager.Add(nameof(SecurityCredentials), async (entity, action) => { await _hubContext.Clients.All.ReceiveSecurityCredentialsUpdateMessage((SecurityCredentials)entity, action); });
        //    _hubManager.Add(nameof(SessionHistory), async (entity, action) => { await _hubContext.Clients.All.ReceiveSessionHistoryUpdateMessage((SessionHistory)entity, action); });
        //    _hubManager.Add(nameof(User), async (entity, action) => { await _hubContext.Clients.All.ReceiveUserUpdateMessage((User)entity, action); });
        //}

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
}
