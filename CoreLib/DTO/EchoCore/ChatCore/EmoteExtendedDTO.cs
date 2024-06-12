using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore;


public class EmoteExtendedDTO : IEmote, IUploadedEmote<UserMinimalDTO>
{
    //public ulong ServerId { get; set; }
    public UserMinimalDTO Uploader { get; set; }
    public ulong Id { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
}
