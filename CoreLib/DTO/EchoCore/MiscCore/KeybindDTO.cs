namespace CoreLib.DTO.EchoCore.MiscCore
{
    public interface IKeybind
    {
        string? Action { get; set; }
        string? Description { get; set; }
        ulong Id { get; set; }
        string Name { get; set; }
    }

    public class KeybindDTO : IKeybind
    {
        public ulong Id { get; set; } //account owner
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Action { get; set; } //example: ALT + i
    }
}
