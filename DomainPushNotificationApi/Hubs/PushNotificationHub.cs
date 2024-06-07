using CoreLib.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DomainPushNotificationApi.Hubs
{
    public class PushNotificationHub : Hub<IPushNotificationHub>
    {
    }
}
