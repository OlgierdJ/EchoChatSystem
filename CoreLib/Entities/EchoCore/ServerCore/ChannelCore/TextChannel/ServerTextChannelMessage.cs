using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessage : BaseMessage<ulong, Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage>
    {
        public ICollection<ServerTextChannelMessageAttachment>? Attachments { get; set; }
        public ServerTextChannelMessagePin? MessagePin { get; set; }
        public ICollection<ServerTextChannelAccountMessageTracker>? MessageTrackers { get; set; }
    }
}
