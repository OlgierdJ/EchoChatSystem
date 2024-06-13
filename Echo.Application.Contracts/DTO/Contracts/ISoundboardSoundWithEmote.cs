using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface ISoundboardSoundWithEmote<TEmote> : ISoundboardSound
{
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    TEmote? AssociatedEmote { get; set; }
}
