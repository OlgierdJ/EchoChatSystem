namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class PermissionCategoryDTO //used to display possible permissions for a role when editing them.
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PermissionExtendedDTO>? Permissions { get; set; }
    }
}
