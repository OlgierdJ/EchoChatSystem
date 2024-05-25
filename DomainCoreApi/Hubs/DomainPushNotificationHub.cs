﻿using CoreLib;
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
           await Clients.Client(Context.ConnectionId).ReceiveNotification($"you are Connected with {Context.User?.Identity?.Name}");
            
           await base.OnConnectedAsync();
        }

        public async Task DmMessage(string Group, MessageDTO Message)
        {
            await Clients.Group(Group).SendDTOMessage(Message);
        }

    }
}
