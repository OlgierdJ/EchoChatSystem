using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore
{
    public class ServerRolePermission : IDomainEntity
    //used for mapping global permissions to a role.
    {
        //pk is combination of role and permission
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerRole Role { get; set; } //Cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}
