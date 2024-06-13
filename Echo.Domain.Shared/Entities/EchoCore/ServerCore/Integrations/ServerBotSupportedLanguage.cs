using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotSupportedLanguage : BaseEntity<ulong>
{
    public ulong LanguageId { get; set; }
    public ulong ServerBotId { get; set; }
    public ServerBot ServerBot { get; set; }
    public Language Language { get; set; }
}
