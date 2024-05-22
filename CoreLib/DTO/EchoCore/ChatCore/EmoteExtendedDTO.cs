using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public interface IEmoteExtended
    {
        ulong Id { get; set; }
        string ImageUrl { get; set; }
        string Name { get; set; }
        UserMinimalDTO Uploader { get; set; }
    }

    public class EmoteExtendedDTO : IEmote, IEmoteExtended
    {
        //public ulong ServerId { get; set; }
        public UserMinimalDTO Uploader { get; set; }
        public ulong Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
