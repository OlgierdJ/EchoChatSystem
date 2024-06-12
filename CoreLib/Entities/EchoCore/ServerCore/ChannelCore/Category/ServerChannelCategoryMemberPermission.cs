using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;

public class ServerChannelCategoryMemberPermission : IDomainEntity
//Used for displaying state of serverpermission for a specific channel for a specific role
{
    //combine foreign key category and role.
    //pk is combination of category, role and permission
    public ulong ChannelCategoryId { get; set; } //where it affects
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }
    public ulong PermissionId { get; set; } //given permission
    public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
    public ServerChannelCategoryMemberSettings MemberSettings { get; set; } //cascade
    public ServerChannelCategory ChannelCategory { get; set; } //ignore
    public Server Server { get; set; } //ignore
    public ServerProfile Profile { get; set; } //ignore
    public ServerPermission Permission { get; set; } //cascade

}
