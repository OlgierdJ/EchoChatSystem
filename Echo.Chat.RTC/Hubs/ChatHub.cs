using DomainRTCApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DomainRTCApi.Hubs;

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
    private readonly HttpClient _httpClient;
    private readonly ChatHubGroupManager _groupManager;
    private readonly ChatHubClientManager _clientManager;
    public ChatHub(ChatHubGroupManager groupManager, ChatHubClientManager clientManager, HttpClient httpClient)
    {
        _groupManager = groupManager;
        _clientManager = clientManager;
        _httpClient = httpClient;
    }
    public async override Task OnConnectedAsync()
    {
        //map context connection id to client account in core api
        //string user = Context.User.Identity.Name;
        //ClientMappings.Add(user, Context.ConnectionId);
        await Clients.Client(Context.ConnectionId).ReceiveMessage(
            $"you are connecting with Id{Context.UserIdentifier}");
        var test1 = Context.Items;
        var test2 = Context.UserIdentifier;
        var test3 = Context.ConnectionId;
        var test4 = Context.User;
        var test5 = Context.User.Identity;
        var test6 = Context.User.Claims;
        var test7 = Context.User.Identities;
        if (_clientManager.ClientConnectionMappings.TryGetValue(Context.UserIdentifier, out string value))
        {
            await Clients.Client(value).KillConnection();
            _clientManager.IgnoreDCEventList.TryAdd(value, false);
        }
        _clientManager.ClientConnectionMappings.AddOrUpdate(Context.UserIdentifier, Context.ConnectionId, (key, oldVal) => Context.ConnectionId); //updates the value of the existing connectionid that is mapped to the clienthandle

        await base.OnConnectedAsync();
    }
    public async override Task OnDisconnectedAsync(Exception? exception)
    {
        //remove mapping of context connection id to client account
        //if this is true then it means the disconnected context was prompted to be killed
        if (_clientManager.IgnoreDCEventList.ContainsKey(Context.ConnectionId))
        {
            _clientManager.IgnoreDCEventList.Remove(Context.ConnectionId, out _); //remove instance consumed
        }
        else
        {
            //This is normal graceful cleanup disconnect
            _clientManager.ClientConnectionMappings.Remove(Context.UserIdentifier, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }

    //Join group => take client and put it in group (if the group is a server)

    private async Task JoinGroup(string connectionId, string clientHandle, string joinedGroupName) //joinedGroupName should be chatId or serverId, channelId //done?
    {
        //join group function
        //var client = await _httpClient.GetFromJsonAsync<ServerProfile>("ServerProfile");

        //check if sender is already part of group and if so then remove them from the group before continuing
        //add the sender to the group
        //proc joingroup event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
        //^(plays sound locally on client and then spawns the joining client from their view)
        if (!_groupManager.ClientGroupMappings.TryAdd(clientHandle, joinedGroupName))
        {
            await Groups.RemoveFromGroupAsync(connectionId, _groupManager.ClientGroupMappings.GetValueOrDefault(clientHandle));
            _groupManager.ClientGroupMappings.Remove(clientHandle);
            _groupManager.ClientGroupMappings.Add(clientHandle, joinedGroupName);
        }
        //await _httpClient.GetAsync("server/user/clientHandle");
        await Groups.AddToGroupAsync(connectionId, joinedGroupName);
        await Clients.Group(joinedGroupName).ParticipantJoinedGroup(joinedGroupName, clientHandle);
    }
    private async Task LeaveGroup(string connectionId, string clientHandle, string groupName) //done?
    {
        //leave group function
        //proc leavegroup event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
        //^(plays sound locally on client and then removes the leaving client from their view)
        //remove the sender from the group
        _groupManager.ClientGroupMappings.Remove(clientHandle);
        await Clients.Group(groupName).ParticipantLeftGroup(groupName, clientHandle);
        await Groups.RemoveFromGroupAsync(connectionId, groupName);
    }




    public async Task InvokeJoinGroup(string groupName)
    {

        //groupnames should be defining as in "{serverId}/{channelId}" this makes it easy for the server to
        //get notifications about everything such that when a user joins a channel they also join servergroup
        //if the groupname does not contain a "/" that means it is a channel???
        if (groupName.Contains('/'))
        {
            var server = groupName.Split("/")[0];
            await JoinGroup(Context.ConnectionId, Context.UserIdentifier, server);
            await JoinGroup(Context.ConnectionId, Context.UserIdentifier, groupName);
        }
        else
        {
            await JoinGroup(Context.ConnectionId, Context.UserIdentifier, groupName);
        }
        //await Clients.Group(groupName).ReceiveMessage(Context.User.Identity.Name + "has joined.");
    }
    public async Task InvokeLeaveGroup(string groupName)
    {
        await LeaveGroup(Context.ConnectionId, Context.UserIdentifier, groupName);
    }

    public async Task InvokeMoveParticipant(string userIdentifier, string groupName)
    {
        //move participant function
        //moved user has to be connected for this to work (not just online on the system but connected to a group within the RTCServer chathub)
        //Also moving user has to be part of the same server and have permissions to move the user (later)
        //idk check coreapi for permission or maybe do it in UI on the client (later)
        var test = Clients.User(userIdentifier);
        if (test == null)
            return;
        var testInGrp = _groupManager.ClientGroupMappings.TryGetValue(userIdentifier, out _);
        if (testInGrp == false)
            return;
        //proc moveparticipant event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
        //^(plays sound locally on client and then removes the leaving client from their view)
        //remove the sender from the previous group
        //add the sender to the specified group
        //log the audit in the database of the coreapi
        //Clients.User(userIdentifier).
        var userConnectionId = _clientManager.ClientConnectionMappings[userIdentifier];
        await LeaveGroup(userConnectionId, userIdentifier, groupName);
        await JoinGroup(userConnectionId, userIdentifier, groupName);
    }

    public async Task InvokeKickParticipant(string user, string groupName)
    {
        //move participant function
        //proc moveparticipant event on all participants in group //(maybe exclude specific participants based on settings to avoid wasting bandwidth)
        //^(plays sound locally on client and then removes the leaving client from their view)
        //remove the sender from the previous group
        //add the sender to the specified group
        //log the audit in the database of the coreapi

        await Clients.Group(groupName).ParticipantLeftGroup(groupName, user);
        //Clients.
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task InvokeStreamToAll(IAsyncEnumerable<byte[]> stream)
    {
        await foreach (var item in stream)
        {
            await Clients.All.ReceiveSoundStream(item);
        }
    }
    public async Task InvokeStreamToGroup(string groupName, IAsyncEnumerable<byte[]> stream)
    {
        //Speak to call function
        //stream to all group participants excluding the sender and deafened participants
        await foreach (var item in stream)
        {
            await Clients.Group(groupName).ReceiveSoundStream(item);
        }
    }

}
