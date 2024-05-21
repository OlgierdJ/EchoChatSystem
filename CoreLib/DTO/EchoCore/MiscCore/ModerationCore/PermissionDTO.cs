namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IPermission : IPermissionMinimal
    {
        string? Description { get; set; }
        string? GroupingName { get; set; }
    }

    public class PermissionDTO : IPermission
    {
        public ulong Id { get; set; }
        public string Name { get; set; } //Example app_view_admin_userinterface
        public string? Description { get; set; }
        public string? GroupingName { get; set; } //taken from category
    }
}
