namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.Event;

public class CancelEventRequestDTO
{
    //public ulong Id { get; set; } //user from jwt
    //public ulong serverId { get; set; } //server from route param
    public ulong EventId { get; set; }
}
