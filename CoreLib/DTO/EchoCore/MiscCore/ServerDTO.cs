using CoreLib.DTO.EchoCore.ChatCore;
using CoreLib.DTO.EchoCore.MiscCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class ServerDTO
    {
        //filled with visible data displayed to the user
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string ImageIconURL { get; set; }
        public bool Muted { get; set; } //idk mayb not needed
        public ICollection<PermissionDTO>? MyPermissions { get; set; }
        public ICollection<ServerRoleDTO>? MyRoles { get; set; }

        public ICollection<ChatDTO>? TextChannels { get; set; }
        public ICollection<VoiceChatDTO>? VoiceChannels { get; set; }
        public ICollection<ChannelCategoryDTO>? Categories { get; set; }
    }
}
