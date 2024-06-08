using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.AppearanceCore
{

    public class ThemeDTO : ITheme
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
