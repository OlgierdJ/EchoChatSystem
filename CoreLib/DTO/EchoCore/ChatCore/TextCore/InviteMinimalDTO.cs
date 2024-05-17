using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public class InviteMinimalDTO
    {
        public InviteType Type { get; set; } //displayed when loading chat.
        public string InviteLink { get; set; } //https://echo.gg/aaCgcBkA //maybe get from message content?? nah anyhow component should hyperlink to this thingimergicky upon clicking title.
    }
}
