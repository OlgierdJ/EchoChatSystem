using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

public class Keybind
{
    public ulong KeybindSettingsId { get; set; }
    public byte ApplicationKeybindId { get; set; }
    public string? Action { get; set; } //example: ALT + i

    public KeybindSettings KeybindSettings { get; set; }
    public ApplicationKeybind ApplicationKeybind { get; set; }
}