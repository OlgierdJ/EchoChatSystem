using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.Enums;

namespace Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;


public class InviteMinimalDTO : IInviteMinimal
{
    public InviteType Type { get; set; } //displayed when loading chat.
    public string InviteLink { get; set; } //https://echo.gg/aaCgcBkA //maybe get from message content?? nah anyhow component should hyperlink to this thingimergicky upon clicking title.
}
