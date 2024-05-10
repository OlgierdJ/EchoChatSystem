using CoreLib;
using CoreLib.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DomainCoreApi.Hubs
{
    public class DomainPushNotificationHub : Hub<IDomainNotificationHub>
    {
    }
}
