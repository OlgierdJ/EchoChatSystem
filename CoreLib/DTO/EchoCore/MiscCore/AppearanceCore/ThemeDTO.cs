namespace CoreLib.DTO.EchoCore.MiscCore.AppearanceCore
{
    public interface ITheme
    {
        uint Id { get; set; }
        string Name { get; set; }
    }

    public class ThemeDTO : ITheme
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
