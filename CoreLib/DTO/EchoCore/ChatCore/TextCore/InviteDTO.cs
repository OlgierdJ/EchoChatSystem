using CoreLib.DTO.Contracts;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{

    public class InviteDTO : IInviteMinimal, IInvite //used for displaying invite to a chat or server when echo scans the chat and finds an invite link.

    {

        public string ImageIconURL { get; set; } //server or chat image displayed when loading invite.
        public string Title { get; set; } //Server name or chat name
        public string Description { get; set; } //for server where to, for chat how many members.
        public string InviteLink { get; set; }
        public InviteType Type { get; set; }
    }


}
