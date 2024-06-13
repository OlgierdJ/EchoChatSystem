namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IUploadedSoundboardSound<TUploader> : ISoundboardSound
{
    public TUploader? Uploader { get; set; }
}
