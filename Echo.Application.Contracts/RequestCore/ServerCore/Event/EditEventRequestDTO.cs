namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore;

public class EditEventRequestDTO
{
    //public ulong Id { get; set; } //user from jwt
    //public ulong serverId { get; set; } //server from route param
    //public ulong EventId { get; set; }
    public DateTime TimeStarts { get; set; }
    public DateTime TimeEnds { get; set; }
}
