using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{
    public class VoiceChatDTO : IChatMinimal
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public int OrderWeight { get; set; }
        public uint UserLimit { get; set; } //default 0 = unlimited, max99 (maybe some other max cause server stability via signalr)
        public bool IsAgeRestricted { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsMuted { get; set; }
        public uint BitRate { get; set; } //in kbps
        public VideoQualityMode VideoQuality { get; set; }
        public RegionDTO Region { get; set; }

        public ICollection<UserMinimalDTO>? ActiveParticipants { get; set; } //null from api, filled by client rtc relation, used for displaying participants

        public ICollection<UserMinimalWithPermissionsDTO>? MemberSettings { get; set; } //displays specific permission settings for a specific user enforced within this chat.
        public ICollection<HierarchalRoleMinimalWithPermissionsDTO>? RoleSettings { get; set; } //displays specific permission settings for a specific role enforced within this chat. ????mayb hierarchalrole?
        public ICollection<PermissionDTO>? UserPermissions { get; set; } //the users permissions within the chat, such as view, edit, move, etc //calculated on server based on permission grants, rebukes and defaults relative to the user
        //public ICollection<RoleMinimalDTO>? Roles { get; set; } //the users role within the chat, such as everyone, admin, owner, etc
        //public ICollection<ServerInviteDTO>? Invites { get; set; } //invites posted to this chat. //maybe extend chatdto to textchanneldto or ignore it.
    }
}
