using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessagePin : IMessagePin<ServerTextChannelMessage, ulong, ServerTextChannel, ulong>
    {
        public ulong PinboardId { get; set; }
        public ulong MessageId { get; set; }
        public ServerTextChannel Pinboard { get; set; }
        public ServerTextChannelMessage Message { get; set; }
    }
}