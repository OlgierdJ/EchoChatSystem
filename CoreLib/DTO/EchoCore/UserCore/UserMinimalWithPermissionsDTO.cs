using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public interface IUserMinimalWithPermissions<TPermission> : IUserMinimal
    {
        ICollection<TPermission> Permissions { get; set; }
    }

    public class UserMinimalWithPermissionsDTO : IUserMinimal, IUserMinimalWithPermissions<StatefulPermissionExtendedDTO>
    {
        public ICollection<StatefulPermissionExtendedDTO> Permissions { get; set; }
        public string DisplayName { get; set; }
        public ulong Id { get; set; }
        public string ImageIconURL { get; set; }
    }
}
