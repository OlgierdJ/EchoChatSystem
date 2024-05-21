using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{
    public interface ISoundboardSoundWithUploader<TUploader> : ISoundboardSound
    {
        public TUploader? Uploader { get; set; }
    }

    public class SoundboardSoundExtendedDTO : ISoundboardSound, ISoundboardSoundWithUploader<UserMinimalDTO>, ISoundboardSoundWithEmote<EmoteDTO>
    {
        public UserMinimalDTO? Uploader { get; set; }
        public EmoteDTO? AssociatedEmote { get; set; }
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string SoundFileUrl { get; set; }
    }
}
