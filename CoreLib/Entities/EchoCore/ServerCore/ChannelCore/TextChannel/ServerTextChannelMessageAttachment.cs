using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessageAttachment : BaseEntity<ulong>, IMessageAttachment<ServerTextChannelMessage, ulong>
    {
        public ulong MessageId { get; set; }
        public string FileLocationURL { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public ServerTextChannelMessage Message { get; set; }
    }
}