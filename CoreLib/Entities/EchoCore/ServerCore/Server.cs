using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class Server : BaseEntity<ulong>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ICollection<ServerSettings>?  Settings { get; set; }
        public ICollection<ServerBotIntegration>?  Integrations { get; set; }

        public ICollection<ServerInvite>? Invites { get; set; }
        public ICollection<ServerEvent>? Events { get; set; }

        public ICollection<ServerChannelCategory>?  ChannelCategories { get; set; } //categories containing channels
        public ICollection<ServerTextChannel>? TextChannels { get; set; } //direct channels
        public ICollection<ServerVoiceChannel>? VoiceChannels { get; set; } //direct channels

        public ICollection<ServerProfile>? Members { get; set; } //Joining a server creates a serverprofile for the member and allows them to change the displayed data which is reflected in the server environment
    }
}
