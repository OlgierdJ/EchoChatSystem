using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

public class Server : BaseEntity<ulong>
{
    public string Name { get; set; }
    //public ulong OwnerId { get; set; } //map to serverowner???? or put in role //restrict owner from leaving server
    public DateTime TimeCreated { get; set; }
    public ICollection<ServerRole>? Roles { get; set; }
    public ICollection<ServerEvent>? Events { get; set; }
    public ICollection<ServerEventParticipancy>? EventParticipancies { get; set; } // no action
    public ICollection<ServerInvite>? Invites { get; set; } //maybe put this into settings
    //public ICollection<ServerVoiceInvite>? VoiceInvites { get; set; } //maybe put this into settings
    public ICollection<AccountServerMute>? Muters { get; set; } //maybe put this into settings
    //public ICollection<ServerVoiceMute>? MutedVoices { get; set; } //maybe put bool flag in serverprofile?
    //public ICollection<ServerDeafen>? DeafenedMembers { get; set; } //maybe put bool flag in serverprofile?
    public ServerSettings? Settings { get; set; }
    //public ServerProfile Owner { get; set; } //restrict user from leaving server

    public ICollection<ServerAuditLog>? AuditLogs { get; set; }
    public ICollection<ServerBan>? BanList { get; set; }
    public ICollection<ServerEmote>? Emotes { get; set; }
    public ICollection<ServerSoundboardSound>? SoundboardSounds { get; set; }

    public ICollection<ServerChannelCategory>? ChannelCategories { get; set; } //Essentially a grouping of voicechannels and or textchannels
    public ICollection<ServerChannelCategoryMemberPermission>? ChannelCategoryMemberPermissions { get; set; } //Essentially a grouping of voicechannels and or textchannels
    public ICollection<ServerChannelCategoryMemberSettings>? ChannelCategoryMemberSettings { get; set; } //Essentially a grouping of voicechannels and or textchannels
    public ICollection<ServerTextChannelMemberPermission>? TextChannelMemberPermissions { get; set; } //direct channels
    public ICollection<ServerTextChannelMemberSettings>? TextChannelMemberSettings { get; set; } //direct channels
    public ICollection<ServerTextChannel>? TextChannels { get; set; } //direct channels
    public ICollection<ServerVoiceChannelMemberPermission>? VoiceChannelMemberPermissions { get; set; } //direct channels
    public ICollection<ServerVoiceChannelMemberSettings>? VoiceChannelMemberSettings { get; set; } //direct channels
    public ICollection<ServerVoiceChannel>? VoiceChannels { get; set; } //direct channels

    public ICollection<ServerProfile>? Members { get; set; } //Joining a server creates a serverprofile for the member and allows them to change the displayed data which is reflected in the server environment
}
