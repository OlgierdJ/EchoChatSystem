using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{

    public class NotificationMessageDTO : INotificationMessage
    {
        public ulong Id { get; set; } //message you've been tagged in, and havent read
    }
}
