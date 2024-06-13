using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;


public class NotificationMessageDTO : INotificationMessage
{
    public ulong Id { get; set; } //message you've been tagged in, and havent read
}
