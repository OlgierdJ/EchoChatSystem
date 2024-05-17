using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerEmoteConfiguration : BaseEntity<ulong>
    {
        public ulong UploaderId { get; set; }
        public ulong ServerId { get; set; }
        public string Name { get; set; } //this is what you have to type to make the emote. must be atleast 2characters long and only contain alphanumeric characters and underscores?
        public string ImageUrl { get; set; } //actual image. must be jpg, png, gif, and max 256kb, 128x128px
        public ServerConfiguration Server { get; set; }
        public Account Uploader { get; set; }
        public ICollection<ServerSoundboardSoundConfiguration>? SoundboardSounds { get; set; }
    }
}