using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class EmoteExtendedDTO : EmoteDTO
    {
        //public ulong ServerId { get; set; }
        public UserMinimalDTO Uploader { get; set; }
    }
}
