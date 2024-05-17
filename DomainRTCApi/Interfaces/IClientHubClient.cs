using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace DomainRTCApi.Interfaces
{
    public interface IClientHubClient
    {
        Task Login(string user);
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task LeaveGroups(string[] groupNames);
        Task OnConnectedAsync();
        Task ReceiveAccountUpdateMessage(Account entity, EntityAction action);
        Task ReceiveAccountSettingsUpdateMessage(AccountSettings entity, EntityAction action);
        //Task ReceiveCategoryImageUpdateMessage(CategoryImage entity, EntityAction action);
        //Task ReceiveCategoryUpdateMessage(Category entity, EntityAction action);
        //Task ReceiveCurrentSessionUpdateMessage(CurrentSession entity, EntityAction action);
        //Task ReceiveIconUpdateMessage(Icon entity, EntityAction action);
        //Task ReceiveIconTemplateUpdateMessage(IconTemplate entity, EntityAction action);
        //Task ReceiveItemCommentUpdateMessage(ItemComment entity, EntityAction action);
        //Task ReceiveItemOrderHistoryUpdateMessage(ItemOrderHistory entity, EntityAction action);
        //Task ReceiveItemUpdateMessage(Item entity, EntityAction action);
        //Task ReceiveLocationUpdateMessage(Location entity, EntityAction action);
        //Task ReceiveLoginAttemptUpdateMessage(LoginAttempt entity, EntityAction action);
        //Task ReceiveModelDetailUpdateMessage(ModelDetail entity, EntityAction action);
        //Task ReceiveModelImageUpdateMessage(ModelImage entity, EntityAction action);
        //Task ReceiveModelUpdateMessage(Model entity, EntityAction action);
        //Task ReceiveOrderCommentUpdateMessage(OrderComment entity, EntityAction action);
        //Task ReceiveOrderDetailCommentUpdateMessage(OrderDetailComment entity, EntityAction action);
        //Task ReceiveOrderDetailUpdateMessage(OrderDetail entity, EntityAction action);
        //Task ReceiveOrderUpdateMessage(Order entity, EntityAction action);
        //Task ReceiveOrderStatusUpdateMessage(OrderStatus entity, EntityAction action);
        //Task ReceiveRoleUpdateMessage(Role entity, EntityAction action);
        //Task ReceiveSecurityCredentialsUpdateMessage(SecurityCredentials entity, EntityAction action);
        //Task ReceiveSessionHistoryUpdateMessage(SessionHistory entity, EntityAction action);
        //Task ReceiveUserUpdateMessage(User entity, EntityAction action);
        Task ReceiveUpdateMessage(string message);
    }
}
