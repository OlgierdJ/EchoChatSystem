using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class ApplicationKeybind : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Keybind>? Keybinds { get; set; }
    }
}
