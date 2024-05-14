using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class InviteDTO //used for displaying invite to a chat or server when echo scans the chat and finds an invite link.
    {
        public InviteType Type { get; set; } //displayed when loading chat.
        public string ImageIconURL { get; set; } //server or chat image displayed when loading invite.
        public string Title { get; set; } //Server name or chat name
        public string Description { get; set; } //for server where to, for chat how many members.
        public string InviteLink { get; set; } //https://echo.gg/aaCgcBkA //maybe get from message content?? nah anyhow component should hyperlink to this thingimergicky upon clicking title.
    }

    public enum InviteType
    {
        Server = 0, Chat = 1
    }
}
