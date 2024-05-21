namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    
    public interface IHierarchalRole : IRoleMinimal
    {
        int Importance { get; set; }
    }

    public class HierarchalRoleMinimalDTO : IRoleMinimal, IHierarchalRole
    {
        public ulong Id { get; set; }
        public int Importance { get; set; } //helps display role by power.
        public string Name { get; set; }
    }
}
