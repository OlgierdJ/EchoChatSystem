using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore
{

    public class KeybindDTO : IKeybind
    {
        public ulong Id { get; set; } //account owner
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; } //example: ALT + i
    }
}
