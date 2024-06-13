using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;


public class HierarchalRoleMinimalDTO : IRoleMinimal, IHierarchalRole, ICustomizableRole
{
    public ulong Id { get; set; }
    public int Importance { get; set; } //helps display role by power.
    public string Name { get; set; }
    public string Colour { get; set; }
    public string IconURL { get; set; }
    public bool AllowAnyoneToMention { get; set; }
    public bool DisplaySeperatelyFromOnlineMembers { get; set; }
    public bool IsAdmin { get; set; }
}
