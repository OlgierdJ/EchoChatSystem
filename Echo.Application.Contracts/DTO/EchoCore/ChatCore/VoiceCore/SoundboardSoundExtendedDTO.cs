using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.UserCore;
using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.ChatCore.VoiceCore;


public class SoundboardSoundExtendedDTO : ISoundboardSound, IUploadedSoundboardSound<UserMinimalDTO>, ISoundboardSoundWithEmote<EmoteDTO>
{
    public UserMinimalDTO? Uploader { get; set; }
    public EmoteDTO? AssociatedEmote { get; set; }
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string SoundFileUrl { get; set; }
}
