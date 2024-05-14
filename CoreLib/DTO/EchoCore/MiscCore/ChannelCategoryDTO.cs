using CoreLib.DTO.EchoCore.ChatCore;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class ChannelCategoryDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChatDTO>? TextChannels { get; set; }
        public ICollection<VoiceChatDTO>? VoiceChannels { get; set; }
    }
}