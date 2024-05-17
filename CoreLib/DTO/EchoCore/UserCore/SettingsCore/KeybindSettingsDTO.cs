using CoreLib.DTO.EchoCore.MiscCore;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class KeybindSettingsDTO
    {
        public ulong Id { get; set; }
        public ICollection<KeybindDTO>? Keybinds { get; set; }
    }
}
