using CoreLib.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DomainPushNotificationApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PushNotificationHub : Hub<IPushNotificationHub>
    {
        public override Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;
            return base.OnConnectedAsync();
        }
    }
}
