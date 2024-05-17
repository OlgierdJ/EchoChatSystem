using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerSoundboardSoundConfiguration : BaseEntity<ulong>
    {
        public ulong ServerId { get; set; }
        public ulong UploaderId { get; set; }
        public ulong? AssociatedEmoteId { get; set; }
        public string Name { get; set; }
        public string SoundFileUrl { get; set; }

        public ServerEmoteConfiguration? AssociatedEmote { get; set; }
        public ServerConfiguration Server { get; set; }
        public Account Uploader { get; set; }
    }
}
