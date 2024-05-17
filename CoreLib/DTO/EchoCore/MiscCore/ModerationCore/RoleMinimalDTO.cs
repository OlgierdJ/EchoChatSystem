namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class RoleMinimalDTO
    {
        public ulong Id { get; set; }
        public int Importance { get; set; } //helps display role by power.
        public string Name { get; set; }
    }
}
