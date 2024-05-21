using CoreLib.DTO.EchoCore.MiscCore;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public interface IKeybindSettings
    {
        ulong Id { get; set; }
        ICollection<KeybindDTO>? Keybinds { get; set; }
    }

    public class KeybindSettingsDTO : IKeybindSettings
    {
        public ulong Id { get; set; }
        public ICollection<KeybindDTO>? Keybinds { get; set; }
    }
}
