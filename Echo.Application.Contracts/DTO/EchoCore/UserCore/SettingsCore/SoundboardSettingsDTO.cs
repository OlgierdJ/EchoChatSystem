namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;

public interface ISoundboardSettings
{
    ulong Id { get; set; }
    byte SoundboardVolume { get; set; }
}

public class SoundboardSettingsDTO : ISoundboardSettings
{
    public ulong Id { get; set; }
    public byte SoundboardVolume { get; set; } //max 200
    //public ulong Soundboard { get; set; } //max 200

    /*
     * implement server specific entrance sounds by choosing server and specific soundboard sound.
     * Entrance sound by server and soundboardsound id in the future
     */

}
