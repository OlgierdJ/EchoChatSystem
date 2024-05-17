namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerRolePermissionConfiguration
    //used for mapping global permissions to a role.
    {
        //pk is combination of role and permission
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerRoleConfiguration Role { get; set; } //Cascade
        public ServerPermissionConfiguration Permission { get; set; } //cascade
    }
}
