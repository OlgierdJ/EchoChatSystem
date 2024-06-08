using CoreLib.DTO.Contracts;
using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;
using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public class ChatDTO : IChatMinimal //shared between chats, textchannels and such cause they are generally the same
    {
        //public ulong Id { get; set; }
        public ulong Id { get; set; }
        public string Name { get; set; }
        public int OrderWeight { get; set; }
        public ulong? LastReadMessageId { get; set; } //from messagetracker used to determine start point when entering chat, which messages to load by pagination. (update locally after first retrieval)
        public bool Muted { get; set; }
        public bool Hidden { get; set; }
        public string? IconUrl { get; set; }
        //public int OrderWeight { get; set; }
        //public DateTime TimeLastInteracted { get; set; }
        public PinboardDTO? Pinboard { get; set; } //mayb remove and keep decoupled?=?
        //public ICollection<NotificationMessageDTO>? TrackedNotifications { get; set; }
        public ICollection<MemberDTO>? Participants { get; set; } //mapped through chatparticipany or textchannel roles
        public ICollection<MessageDTO>? Messages { get; set; } //mapped through chatparticipany or textchannel roles
        public ICollection<PermissionDTO>? UserPermissions { get; set; } //the users permissions within the chat, such as view, edit, move, etc //calculated on server based on permission grants, rebukes and defaults relative to the user
        //public ICollection<RoleMinimalDTO>? Roles { get; set; } //the users role within the chat, such as everyone, admin, owner, etc
        public ICollection<UserMinimalWithPermissionsDTO>? MemberSettings { get; set; } //displays specific permission settings for a specific user enforced within this chat.
        public ICollection<HierarchalRoleMinimalWithPermissionsDTO>? RoleSettings { get; set; } //displays specific permission settings for a specific role enforced within this chat.
        public string? CategoryName { get; set; }
        //public ICollection<ServerInviteDTO>? Invites { get; set; } //invites posted to this chat. //maybe extend chatdto to textchanneldto or ignore it.
    }
}
