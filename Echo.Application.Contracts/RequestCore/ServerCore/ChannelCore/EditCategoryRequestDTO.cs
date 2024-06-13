namespace Echo.Application.Contracts.DTO.RequestCore.ServerCore.ChannelCore;

public class EditCategoryRequestDTO
{
    //public ulong userId { get; set; } //jwt
    //public ulong serverId { get; set; } //route
    //public ulong Id { get; set; } //route
    public string Name { get; set; }
    public bool IsPrivate { get; set; }
}
