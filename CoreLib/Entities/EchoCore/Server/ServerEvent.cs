using CoreLib.Entities.Base;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.Server
{
    public class ServerEvent : BaseEntity<ulong>
    {
        public string Topic { get; set; }
        public string? Description { get; set; } //optional
        public string? ImageFileUrl { get; set; } //optional
        public EventFrequency EventFrequency { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; } //if it has voicechannellocation then it shouldnt have location
        public ServerVoiceChannel? VoiceChannelLocation { get; set; } //if it has location then it shouldnt have voicechannellocation
    }
    
}