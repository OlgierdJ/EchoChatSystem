using CoreLib;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace DomainCoreApi.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DomainPushNotificationHub : Hub<IDomainNotificationHub>
    {
        public override async Task OnConnectedAsync()
        {
           //await Clients.Client(Context.ConnectionId).ReceiveNotification($"you are Connected with {Context.User?.Identity?.Name}");
            
           await base.OnConnectedAsync();
        }

        public async Task JoinGroup(string groupName)
        {
            //await Clients.Caller.JoinGroup(groupName);
        }

        public async Task JoinGroups(string[] groupNames)
        {
            //await Clients.Caller.JoinGroups(groupNames);
        }
        public async Task LeaveGroup(string groupName)
        {
            //await Clients.Caller.LeaveGroup(groupName);
        }
        public async Task LeaveGroups(string[] groupNames)
        {
            //await Clients.Caller.LeaveGroups(groupNames);
        }


        //public async Task ReceiveChatMessageCreateMessageDTO(MessageDTO entity,string Group)
        //{
        //    await Clients.Group(Group).SendDTOMessage(entity);
        //}
        //public async Task ReceiveChatMessageUpdateMessageDTO(MessageDTO entity, string Group)
        //{
        //    await Clients.Group(Group).SendDTOMessage(entity);
        //}
        //public async Task ReceiveChatMessageDeleteMessageDTO(MessageDTO entity, string Group)
        //{
        //    await Clients.Group(Group).SendDTOMessage(entity);
        //}

        //public async Task SendDTOMessage(MessageDTO entity)
        //{

        //}

        //public async Task DmMessage(string Group, MessageDTO Message)
        //{
        //    await Clients.Group(Group).SendDTOMessage(Message);
        //}

    }
}
