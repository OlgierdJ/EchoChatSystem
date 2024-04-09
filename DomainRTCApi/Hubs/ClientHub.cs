using DomainRTCApi.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DomainRTCApi.Hubs
{
    public class ClientHub : Hub<IClientHubClient>
    {

        //public override Task OnConnectedAsync()
        //{
        //    Console.WriteLine(Context.User.Identity.Name);
        //    foreach (var item in Context.User.Identities)
        //    {
        //        Console.WriteLine(item.Name);
        //    }
        //    return base.OnConnectedAsync();
        //}

        //public async Task JoinGroup(string groupName)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Caller.JoinGroup(groupName);
        //}

        //public async Task LeaveGroups(string[] groupNames)
        //{
        //    foreach (var groupName in groupNames)
        //    {
        //        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    }
        //    await Clients.Caller.LeaveGroups(groupNames);
        //}

        //public async Task LeaveGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Caller.LeaveGroup(groupName);
        //}

        //public async Task SendUpdateMessage(string message, string action)
        //{
        //    await Clients.All.ReceiveUpdateMessage(message, action);
        //}
        //public async Task SendCategoryUpdateMessage(Category category, string action)
        //{
        //    await Clients.All.ReceiveCategoryUpdateMessage(category, action);
        //}
        //public async Task SendCommentUpdateMessage(Comment comment, string action)
        //{
        //    await Clients.All.ReceiveCommentUpdateMessage(comment, action);
        //}
        //public async Task SendItemUpdateMessage(Item item, string action)
        //{
        //    await Clients.All.ReceiveItemUpdateMessage(item, action);
        //}
        //public async Task SendLocationUpdateMessage(Location location, string action)
        //{
        //    await Clients.All.ReceiveLocationUpdateMessage(location, action);
        //}
        //public async Task SendModelUpdateMessage(Model model, string action)
        //{
        //    await Clients.All.ReceiveModelUpdateMessage(model, action);
        //}
        //public async Task SendOrderUpdateMessage(Order order, string action)
        //{
        //    await Clients.All.ReceiveOrderUpdateMessage(order, action);
        //}
        //public async Task SendUserUpdateMessage(User user, string action)
        //{
        //    await Clients.All.ReceiveUserUpdateMessage(user, action);
        //}
    }
}
