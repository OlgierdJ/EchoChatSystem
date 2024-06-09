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
            //pull client chats and servers from api or db directly
            //add client to all groups they need i.e $"{typeof(Chat)}/{chat.Id}"
            var user = Context.User.Identity.Name;
            return base.OnConnectedAsync();
        }
    }
}
