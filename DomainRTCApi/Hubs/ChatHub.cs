using DomainRTCApi.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;

namespace DomainRTCApi.Hubs
{
    public class ChatHub : Hub<IClientChat>
    {
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-8.0
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-8.0#use-claims-to-customize-identity-handling
        #region docs for Stream data
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
        public ChatHubGroupManager GroupManager { get; set; }
        public ChatHubClientManager ClientManager { get; set; }
        public Dictionary<string, string> ClientMappings { get; set; }
        public ChatHub(ChatHubGroupManager gm, ChatHubClientManager cm)
        { 
            GroupManager = gm;
            ClientManager = cm;
        }

        private async Task JoinGroup(string clientHandle, string groupName)
        {
            //join group function
            //add the sender to the group
            //proc joingroup event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
            //^(plays sound locally on client and then spawns the joining client from their view)
            if (!GroupManager.ClientGroupMappings.TryAdd(clientHandle, groupName))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupManager.ClientGroupMappings.GetValueOrDefault(clientHandle));
                GroupManager.ClientGroupMappings.Remove(clientHandle);
                GroupManager.ClientGroupMappings.Add(clientHandle, groupName);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).JoinGroup(groupName, clientHandle);
        }
        private async Task LeaveGroup(string connectionId, string clientHandle, string groupName)
        {
            //leave group function
            //proc leavegroup event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
            //^(plays sound locally on client and then removes the leaving client from their view)
            //remove the sender from the group
            GroupManager.ClientGroupMappings.Remove(Context.UserIdentifier);
            await Clients.Group(groupName).LeaveGroup(groupName, Context.UserIdentifier);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async override Task OnConnectedAsync()
        {
            //map context connection id to client account in core api
            //string user = Context.User.Identity.Name;
            //ClientMappings.Add(user, Context.ConnectionId);
            await Clients.Client(Context.ConnectionId).ReceiveMessage(
                $"you are connecting with Id{Context.User?.Identity?.Name}");
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            //remove mapping of context connection id to client account
            await base.OnDisconnectedAsync(exception);
        }

        #region Group methods 
        public async Task InvokeJoinGroup(string groupName)
        {
            await JoinGroup(Context.UserIdentifier, groupName);
            //await Clients.Group(groupName).ReceiveMessage(Context.User.Identity.Name + "has joined.");
        }
        public async Task InvokeLeaveGroup(string groupName)
        {
            
        }
        #endregion
        public async Task MoveParticipant(string userIdentifier, string groupName)
        {
            //move participant function
            //moved user has to be connected for this to work (not just online on the system but connected to a group within the RTCServer chathub)
            //Also moving user has to be part of the same server and have permissions to move the user (later)
            //idk check coreapi for permission or maybe do it in UI on the client (later)
            var test = Clients.User(userIdentifier);
            if (test == null)
                return;
            var testInGrp = GroupManager.ClientGroupMappings.TryGetValue(userIdentifier, out _);
            if (testInGrp == false)
                return;
            //proc moveparticipant event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
            //^(plays sound locally on client and then removes the leaving client from their view)
            //remove the sender from the previous group
            //add the sender to the specified group
            //log the audit in the database of the coreapi

            if (!GroupManager.ClientGroupMappings.TryAdd(userIdentifier, groupName))
            {
                await Groups.RemoveFromGroupAsync(userIdentifier, GroupManager.ClientGroupMappings.GetValueOrDefault(userIdentifier));
                GroupManager.ClientGroupMappings.Remove(Context.UserIdentifier);
                GroupManager.ClientGroupMappings.Add(Context.UserIdentifier, groupName);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).JoinGroup(groupName);

            await Clients.Group(groupName).LeaveGroup(groupName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task KickParticipant(string user, string groupName)
        {
            //move participant function
            //proc moveparticipant event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
            //^(plays sound locally on client and then removes the leaving client from their view)
            //remove the sender from the previous group
            //add the sender to the specified group
            //log the audit in the database of the coreapi

            await Clients.Group(groupName).LeaveGroup(groupName);
            Clients.
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        #region Voice methods
        public async Task StreamToAll(IAsyncEnumerable<byte[]> stream)
        {
            await foreach (var item in stream)
            {
                await Clients.All.ReceiveSoundStream(item);
            }
        }
        public async Task StreamToGroup(string groupName, IAsyncEnumerable<byte[]> stream)
        {
           //Speak to call function
           //stream to all group participants excluding the sender and deafened participants
           await foreach(var item in stream)
            {
                await Clients.Group(groupName).ReceiveSoundStream(item);
            }
        }
        #endregion
    }
}
