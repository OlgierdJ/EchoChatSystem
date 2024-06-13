using Echo.Application.Contracts.DTO.EchoCore.UserCore;

namespace Echo.Application.Contracts.DTO.EchoCore.ServerCore;

public class ServerInviteDTO //used for displaying and allowing cancellations of server invites.
{
    public string InviteCode { get; set; }
    public string? Location { get; set; } //where it navigates the user upon joining.
    public int TimesUsed { get; set; } //where it navigates the user upon joining.
    public int TotalUses { get; set; } //where it navigates the user upon joining.
    public UserMinimalDTO User { get; set; }
    public DateTime? ExpirationTime { get; set; }
}
