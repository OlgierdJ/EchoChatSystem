using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Hubs;
using DomainPushNotificationApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using static System.Net.WebRequestMethods;

namespace DomainPushNotificationApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PushNotificationHub : Hub<IPushNotificationHub>
    {
        private readonly PushNotificationDomainApiService service;
        private readonly PushNotificationClientConnectionStore userConnectionStore;

        public PushNotificationHub(PushNotificationDomainApiService service, PushNotificationClientConnectionStore connectionStore)
        {
            this.service = service;
            this.userConnectionStore = connectionStore;
        }
        public override async Task OnConnectedAsync()
        {
            //pull client chats and servers from api or db directly
            //add client to all groups they need i.e $"{typeof(Chat)}/{chat.Id}"
            var userId = Convert.ToUInt64(Context.User.Identity.Name);
            userConnectionStore.AddUserConnection(userId, Context.ConnectionId);
            var grps = await service.GetGroupsByAccountId(userId);
            foreach (var grp in grps)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, grp);
            }
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Convert.ToUInt64(Context.User.Identity.Name);
            userConnectionStore.RemoveUserConnection(userId, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
