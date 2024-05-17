using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore.VoiceCore
{
    public class SoundboardSoundExtendedDTO : SoundboardSoundDTO
    {
        public UserMinimalDTO? Uploader { get; set; }
    }
}
