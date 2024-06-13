namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.ChannelCore;

public class MoveChannelRequestDTO
{
    //public ulong userId { get; set; } //get from jwt
    //public ulong serverId { get; set; } //get from route param
    //public ulong channelId { get; set; } //get from route param
    public int OrderWeight { get; set; }
    public ulong? CategoryId { get; set; }
}
