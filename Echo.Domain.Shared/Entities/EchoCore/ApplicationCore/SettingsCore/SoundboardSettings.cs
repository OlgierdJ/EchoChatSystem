using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class SoundboardSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public byte SoundboardVolume { get; set; } //max 200
    //public ulong Soundboard { get; set; } //max 200

    /*
     * implement server specific entrance sounds by choosing server and specific soundboard sound.
     * Entrance sound by server and soundboardsound id in the future
     */

    public AccountSettings AccountSettings { get; set; }
}
