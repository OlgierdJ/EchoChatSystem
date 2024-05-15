using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.DTO.EchoCore.AccountCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore
{
    public class ServerConfiguration : BaseEntity<ulong>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ICollection<ServerEvent>? Events { get; set; }
        public ICollection<ServerInvite>? Invites { get; set; } //maybe put this into settings
        public ICollection<AccountServerMute>? Muters { get; set; } //maybe put this into settings
        public ServerSettings? Settings { get; set; }

        public ICollection<ServerAuditLog>? AuditLogs { get; set; }
        public ICollection<ServerBan>? BanList { get; set; }
        public ICollection<ServerEmote>? Emotes { get; set; }
        public ICollection<ServerSoundboardSound>? SoundboardSounds { get; set; }

        public ICollection<ServerChannelCategory>? ChannelCategories { get; set; } //Essentially a grouping of voicechannels and or textchannels
        public ICollection<ServerTextChannel>? TextChannels { get; set; } //direct channels
        public ICollection<ServerVoiceChannel>? VoiceChannels { get; set; } //direct channels

        public ICollection<ServerProfile>? Members { get; set; } //Joining a server creates a serverprofile for the member and allows them to change the displayed data which is reflected in the server environment
    }
}
