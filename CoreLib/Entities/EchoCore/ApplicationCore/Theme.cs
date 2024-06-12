using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;

namespace CoreLib.Entities.EchoCore.ApplicationCore;

public class Theme : BaseEntity<uint>
{
    public string Name { get; set; }
    public IEnumerable<AppearanceSettings>? AppearanceSettings { get; set; }
}
