using CoreLib.DTO.EchoCore.MiscCore.AppearanceCore;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class AppearanceSettingsDTO
    {
        public ulong Id { get; set; }
        public uint ThemeId { get; set; }
        public string InAppIcon { get; set; }
        public bool DarkSideBar { get; set; }
        public MessageDisplayMode MessageDisplayMode { get; set; }
        public bool ShowAvatarsInCompactMode { get; set; }
        public byte PixelChatFontScale { get; set; }
        public byte PixelSpaceBetweenMessageGroupsScale { get; set; }
        public byte ZoomLevel { get; set; }

        public ThemeDTO Theme { get; set; }
    }

}
