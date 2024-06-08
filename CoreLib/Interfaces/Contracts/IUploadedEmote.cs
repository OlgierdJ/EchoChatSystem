using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.Interfaces.Contracts
{
    public interface IUploadedEmote<TUploader> : IEmote
    {
        //ulong Id { get; set; } same as iemote
        TUploader Uploader { get; set; }
    }
}
