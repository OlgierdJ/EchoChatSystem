using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;

public class ServerChannelCategoryMemberSettings : IDomainEntity
{
    public ulong ChannelCategoryId { get; set; } //where these settings belong

    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }


    public ServerChannelCategory ChannelCategory { get; set; }
    public ServerProfile Profile { get; set; }
    public Server Server { get; set; } //ignore
    public ICollection<ServerChannelCategoryMemberPermission>? Permissions { get; set; }
}