using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class Permission : BaseEntity<ulong>, IRolePermission<Role>
    {
        public ICollection<Role>? Roles { get; set; }
        public string Name { get; set; }
    }
}
