using CoreLib.Interfaces.Contracts;

namespace CoreLib.DTO.Contracts;

public interface ISoundboardSoundWithEmote<TEmote> : ISoundboardSound
{
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    TEmote? AssociatedEmote { get; set; }
}
