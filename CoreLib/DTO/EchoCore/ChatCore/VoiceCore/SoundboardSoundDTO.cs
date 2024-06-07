using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{

    public class SoundboardSoundDTO : ISoundboardSoundWithEmote<EmoteDTO>
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string SoundFileUrl { get; set; } //this is the sound that should play when the user clicks the sound.
        public EmoteDTO? AssociatedEmote { get; set; }

    }


}
