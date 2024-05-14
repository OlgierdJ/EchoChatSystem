using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.DTO.EchoCore.FriendCore.UserCore.MiscCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SettingsCore
{
    public class KeybindSettingsDTO
    {
        public ulong Id { get; set; }
        public ICollection<KeybindDTO>? Keybinds { get; set; }
    }
}
