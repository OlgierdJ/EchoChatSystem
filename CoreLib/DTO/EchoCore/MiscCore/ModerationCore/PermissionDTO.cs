namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class PermissionDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; } //Example app_view_admin_userinterface
        public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
    }
}
