using CoreLib.Interfaces.Contracts;

namespace CoreLib.DTO.Contracts
{
    public interface ISoundboardSoundWithEmote<TEmote> : ISoundboardSound
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string Name { get; set; }
        string SoundFileUrl { get; set; }
        TEmote? AssociatedEmote { get; set; }
    }


}
