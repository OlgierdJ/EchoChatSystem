using CoreLib.Entities.Base;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations
{
    public class ServerBotImage : BaseEntity<ulong>, IOwnedImage<ServerBot, ulong>
    {
        public ulong Id { get; set; }
        public ulong OwnerId { get; set; }
        public ServerBot Owner { get; set; }
        public string ImageURL { get; set; }
        public uint Importance { get; set; }
    }
}