using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ServerConfiguration  Server { get; set; }
        public Account Uploader { get; set; }
    }
}
