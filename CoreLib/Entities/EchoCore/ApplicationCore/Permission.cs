using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class Permission : BaseEntity<ulong>, IRolePermission<Role>
    {
        public ICollection<Role>? Roles { get; set; }
        public string Name { get; set; }
    }
}
