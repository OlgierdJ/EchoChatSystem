using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

public class ServerSoundboardSound : BaseEntity<ulong>
{
    public ulong ServerId { get; set; }
    public ulong UploaderId { get; set; }
    public ulong? AssociatedEmoteId { get; set; }
    public string Name { get; set; }
    public string SoundFileUrl { get; set; }
    public byte Volume { get; set; } //max 200 ? maybe 100?

    public ServerEmote? AssociatedEmote { get; set; }
    public Server Server { get; set; }
    public Account Uploader { get; set; }
}
