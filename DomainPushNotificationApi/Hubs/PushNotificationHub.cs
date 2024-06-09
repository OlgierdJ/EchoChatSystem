using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Hubs;
using DomainPushNotificationApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static System.Net.WebRequestMethods;

namespace DomainPushNotificationApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PushNotificationHub : Hub<IPushNotificationHub>
    {
        private readonly PushNotificationDomainApiService service;

        public PushNotificationHub(PushNotificationDomainApiService service)
        {
            this.service = service;
        }
        public override async Task OnConnectedAsync()
        {
            //pull client chats and servers from api or db directly
            //add client to all groups they need i.e $"{typeof(Chat)}/{chat.Id}"
            var userId = Convert.ToUInt64(Context.User.Identity.Name);
            var grps = await service.GetGroupsByAccountId(userId);
            foreach (var grp in grps)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, grp);
            }
            await base.OnConnectedAsync();
        }
    }
}
