using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore
{
    public class ServerRegion : BaseEntity<uint>
    {
        //displayed name of the region
        public string Name { get; set; }
        //brazil flag.icon
        public string Icon { get; set; }

        //url of the hub server responsible for the region - maybe in the future specify reverse proxy api manager which could serve different apis based on health
        public string RegionServerURL { get; set; }
        public ICollection<ServerVoiceChannel>? VoiceChannels { get; set; } //voicechannels which has specified specific region of which to traffic voice through
        public ICollection<ServerSettings>? ServerSettings { get; set; }  //server global setting of where to traffic notifications through
    }
}