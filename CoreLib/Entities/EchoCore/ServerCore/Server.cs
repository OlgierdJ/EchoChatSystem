using CoreLib.Entities.Base;
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
        public List<ServerSettings>  Settings { get; set; }

        public List<ServerInvite> Invites { get; set; }
        public List<ServerEvent> Events { get; set; }

        public List<ServerChannelCategory>  ChannelCategories { get; set; } //categories containing channels
        public List<ServerTextChannel> TextChannels { get; set; } //direct channels
        public List<ServerVoiceChannel> VoiceChannels { get; set; } //direct channels

        public List<ServerProfile> Members { get; set; } //Joining a server creates a serverprofile for the member and allows them to change the displayed data which is reflected in the server environment
    }
}
