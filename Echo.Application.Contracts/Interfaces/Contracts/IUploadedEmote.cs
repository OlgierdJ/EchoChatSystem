namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IUploadedEmote<TUploader> : IEmote
{
    //ulong Id { get; set; } same as iemote
    TUploader Uploader { get; set; }
}
