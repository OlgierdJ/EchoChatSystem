using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

public class Theme : BaseEntity<uint>
{
    public string Name { get; set; }
    public IEnumerable<AppearanceSettings>? AppearanceSettings { get; set; }
}
