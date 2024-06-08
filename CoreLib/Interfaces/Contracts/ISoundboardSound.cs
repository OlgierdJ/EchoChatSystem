namespace CoreLib.Interfaces.Contracts
{
    public interface ISoundboardSound
    {
        //ulong Id { get; set; } inherit from iidentified or ientity interface instead.
        string Name { get; set; }
        string SoundFileUrl { get; set; }
    }


}
