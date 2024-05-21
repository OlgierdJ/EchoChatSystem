namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{
    public interface ISoundboardSound
    {
        ulong Id { get; set; }
        string Name { get; set; }
        string SoundFileUrl { get; set; }
    }

    public interface ISoundboardSoundWithEmote<TEmote> : ISoundboardSound
    {
        ulong Id { get; set; }
        string Name { get; set; }
        string SoundFileUrl { get; set; }
        TEmote? AssociatedEmote { get; set; }
    }

    public class SoundboardSoundDTO : ISoundboardSoundWithEmote<EmoteDTO>
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string SoundFileUrl { get; set; } //this is the sound that should play when the user clicks the sound.
        public EmoteDTO? AssociatedEmote { get; set; }

    }


}
