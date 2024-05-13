using CoreLib.Entities.Base;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessagePinConfiguration : BaseMessagePin<ServerTextChannelMessageConfiguration, ulong, ServerTextChannelPinboardConfiguration, ulong>
    {
    }
}