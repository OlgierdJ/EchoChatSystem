namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.ChannelCore;

public class UpdateTextChannelRequestDTO
{
    //public ulong userId { get; set; } //from jwt
    //public ulong serverid { get; set; } //from route
    //public ulong channelid { get; set; } //from route
    public string Name { get; set; }
    public string? Topic { get; set; }
    public int SlowMode { get; set; }
    public bool IsAgeRestricted { get; set; }
    public bool IsPrivate { get; set; }
}
