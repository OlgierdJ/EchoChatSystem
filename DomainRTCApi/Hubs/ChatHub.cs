using DomainRTCApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;

namespace DomainRTCApi.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IClientChat>
    {

        #region docs for Stream data
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-8.0
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-8.0#use-claims-to-customize-identity-handling
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/streaming?view=aspnetcore-8.0
        //https://github.com/Shhzdmrz/SignalRCoreWebRTC/tree/master
        //public async Task UploadStream(ChannelReader<string> stream)
        //{
        //    while (await stream.WaitToReadAsync())
        //    {
        //        while (stream.TryRead(out var item))
        //        {
        //            // do something with the stream item
        //            Console.WriteLine(item);
        //        }
        //    }
        //}

        //public async Task UploadStream(IAsyncEnumerable<string> stream)
        //{
        //    await foreach (var item in stream)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}
        #endregion

        public async override Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ReceiveMessage(
                $"you are connecting with Id {Context.User.Identity.Name}");

            await base.OnConnectedAsync();
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.JoinGroup(groupName);
            await Clients.Group(groupName).ReceiveMessage(Context.User.Identity.Name + "has joined.");
        }
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.LeaveGroup(groupName);
        }
        public async Task StreamToAll(IAsyncEnumerable<byte[]> stream)
        {
            await foreach (var item in stream)
            {
                await Clients.All.ReceiveSoundStream(item);
            }
        }
        public async Task StreamToGroup(string groupName, IAsyncEnumerable<byte[]> stream)
        {
           await foreach(var item in stream)
            {
                await Clients.Group(groupName).ReceiveSoundStream(item);
            }
        }
    }
}
