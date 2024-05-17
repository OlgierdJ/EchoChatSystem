using CoreLib;
using Microsoft.AspNetCore.SignalR;

namespace DomainCoreApi.Hubs
{
    public class DomainPushNotificationHub : Hub<IDomainNotificationHub>
    {
    }
}
