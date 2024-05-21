namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface INotificationMessage
    {
        ulong Id { get; set; }
    }

    public class NotificationMessageDTO : INotificationMessage
    {
        public ulong Id { get; set; } //message you've been tagged in, and havent read
    }
}
