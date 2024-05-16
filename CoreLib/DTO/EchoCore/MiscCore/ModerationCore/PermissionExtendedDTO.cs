namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class PermissionExtendedDTO : PermissionDTO
    {
        public string GroupingName { get; set; } //taken from category
        public string? Description { get; set; }
    }
}
