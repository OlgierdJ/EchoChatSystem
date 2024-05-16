using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.ChatCore.VoiceCore;
using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ChannelCategoryDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChatDTO>? TextChannels { get; set; }
        public ICollection<VoiceChatDTO>? VoiceChannels { get; set; }
        public ICollection<PermissionDTO>? Permissions { get; set; } //the users permissions within the channelcategory, such as view, edit, move, etc

        public ICollection<UserWithPermissionsDTO>? MemberSettings { get; set; } //displays specific permission settings for a specific user enforced within this chat.
        public ICollection<RoleDTO>? RoleSettings { get; set; } //displays specific permission settings for a specific role enforced within this chat.
    }
}