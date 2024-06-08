using CoreLib.DTO.Contracts;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{

    public class SoundboardSoundExtendedDTO : ISoundboardSound, IUploadedSoundboardSound<UserMinimalDTO>, ISoundboardSoundWithEmote<EmoteDTO>
    {
        public UserMinimalDTO? Uploader { get; set; }
        public EmoteDTO? AssociatedEmote { get; set; }
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string SoundFileUrl { get; set; }
    }
}
