using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class AppearanceSettings : BaseEntity<ulong>
    {
        public ulong AccountSettingsId { get; set; }
        public uint ThemeId { get; set; }
        public string InAppIcon { get; set; }
        public bool DarkSideBar { get; set; }
        public MessageDisplayMode MessageDisplayMode { get; set; }
        public bool ShowAvatarsInCompactMode { get; set; }
        public byte PixelChatFontScale { get; set; }
        public byte PixelSpaceBetweenMessageGroupsScale { get; set; }
        public byte ZoomLevel { get; set; }

        public Theme Theme { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }

}
