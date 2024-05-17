namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class RoleMinimalWithPermissionsDTO : RoleMinimalDTO
    {
        public ICollection<PermissionExtendedDTO> Permissions { get; set; }
    }
}
