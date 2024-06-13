namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.Event;

public class JoinEventRequestDTO
{
    //public ulong Id {  get; set; } //get from jwt
    //public ulong serverId { get; set; } //server from route param
    public ulong EventId { get; set; }
}
