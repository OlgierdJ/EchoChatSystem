using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.Server
{
    public class Server : BaseEntity<string>
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ServerSettings>  ServerSettings { get; set; }
        public List<ChannelCategory>  ChannelCategories { get; set; } //categories containing channels

        public List<ServerTextChannel> ServerTextChannel { get; set; } //direct channels
        public List<ServerVoiceChannel> ServerVoiceChannel { get; set; } //direct channels
    }
}
