using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.AppearanceCore;


public class ThemeDTO : ITheme
{
    public uint Id { get; set; }
    public string Name { get; set; }
}
