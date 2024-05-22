using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface IInviteMinimal
    {
        string InviteLink { get; set; }
        InviteType Type { get; set; }
    }

    public class InviteMinimalDTO : IInviteMinimal
    {
        public InviteType Type { get; set; } //displayed when loading chat.
        public string InviteLink { get; set; } //https://echo.gg/aaCgcBkA //maybe get from message content?? nah anyhow component should hyperlink to this thingimergicky upon clicking title.
    }
}
