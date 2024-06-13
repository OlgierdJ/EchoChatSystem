namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore;

public class ServerKickUserRequestDTO //kicks user from server
{
    //public ulong adminid { get; set; } //from jwt
    public ulong UserId { get; set; }
    public string? Reason { get; set; } // for audit log
}
