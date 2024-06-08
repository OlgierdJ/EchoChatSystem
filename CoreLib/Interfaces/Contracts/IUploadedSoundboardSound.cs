namespace CoreLib.Interfaces.Contracts
{
    public interface IUploadedSoundboardSound<TUploader> : ISoundboardSound
    {
        public TUploader? Uploader { get; set; }
    }
}
