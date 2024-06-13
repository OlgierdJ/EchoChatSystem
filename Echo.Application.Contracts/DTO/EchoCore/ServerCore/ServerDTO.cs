using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.ChatCore;
using Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;
using Echo.Application.Contracts.DTO.EchoCore.ChatCore.VoiceCore;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore;

namespace Echo.Application.Contracts.DTO.EchoCore.ServerCore;

public class ServerDTO : IServerMinimal
{
    //filled with visible data displayed to the user
    //public ulong Id { get; set; }
    //public string Name { get; set; }
    //public string ImageIconURL { get; set; }
    public ulong Id { get; set; }
    public string ImageIconURL { get; set; }
    public string Name { get; set; }
    public bool Muted { get; set; } //idk mayb not needed
    //the users permissions within the server, such as view, edit, move, etc 
    //calculated on server based on permission grants, rebukes and defaults relative to the user
    public ICollection<PermissionDTO>? UserPermissions { get; set; } //does not need display cause only for granting access //permissions are filtered by x/or-ing them together with granted roles from rolesettings
    //this is only used for determining whether or not the user can act based on their permissions towards another member if they have lower, same or higher role importance
    public ICollection<HierarchalRoleMinimalDTO>? UserRoles { get; set; } //the users role within the server, such as everyone, admin, owner, etc //does not need display cause only for calculation

    public ICollection<UserMinimalWithPermissionsDTO>? MemberSettings { get; set; } //specific members with their global permissions
    public ICollection<HierarchalRoleMinimalWithPermissionsDTO>? RoleSettings { get; set; } //all roles with their global permissions

    public ICollection<ChatDTO>? TextChannels { get; set; }
    public ICollection<VoiceChatDTO>? VoiceChannels { get; set; }
    public ICollection<ChannelCategoryDTO>? Categories { get; set; }


    public ServerSettingsDTO? Settings { get; set; }

    public ICollection<ServerEventDTO>? Events { get; set; }
    public ICollection<ServerInviteDTO>? Invites { get; set; } //maybe put this into settings
    //public ICollection<AccountServerMute>? Muters { get; set; } //not needed on client side.

    public ICollection<AuditLogDTO>? AuditLogs { get; set; }
    public ICollection<BanDTO>? BanList { get; set; }
    public ICollection<EmoteDTO>? Emotes { get; set; }
    public ICollection<SoundboardSoundDTO>? SoundboardSounds { get; set; }

    //maybe hide or ignore this list as the server will only display active chat members.
    public ICollection<MemberDTO>? Members { get; set; } //Joining a server creates a serverprofile for the member and allows them to change the displayed data which is reflected in the server environment
}
