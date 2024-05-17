using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserMinimalWithPermissionsDTO : UserMinimalDTO
    {
        public ICollection<PermissionExtendedDTO> Permissions { get; set; }
    }
}
