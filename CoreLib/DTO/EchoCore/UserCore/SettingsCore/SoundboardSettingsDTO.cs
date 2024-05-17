namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class SoundboardSettingsDTO
    {
        public ulong Id { get; set; }
        public byte SoundboardVolume { get; set; } //max 200
        //public ulong Soundboard { get; set; } //max 200

        /*
         * implement server specific entrance sounds by choosing server and specific soundboard sound.
         * Entrance sound by server and soundboardsound id in the future
         */

    }
}
