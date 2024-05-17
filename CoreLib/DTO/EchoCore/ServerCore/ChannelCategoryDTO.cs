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

        public ICollection<PermissionDTO>? UserPermissions { get; set; } //the users permissions within the category, such as view, edit, move, etc //calculated on server based on permission grants, rebukes and defaults relative to the user
        //public ICollection<RoleMinimalDTO>? Roles { get; set; } //the users role within the category, such as everyone, admin, owner, etc

        public ICollection<UserMinimalWithPermissionsDTO>? MemberSettings { get; set; } //displays specific permission settings for a specific user enforced within this chat.
        public ICollection<RoleMinimalWithPermissionsDTO>? RoleSettings { get; set; } //displays specific permission settings for a specific role enforced within this chat.
    }
}