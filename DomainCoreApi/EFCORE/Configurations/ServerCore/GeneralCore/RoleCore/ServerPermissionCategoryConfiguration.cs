using CoreLib.Entities.Base;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionCategoryConfiguration : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ServerPermissionConfiguration>? Permissions { get; set; }
    }
}
