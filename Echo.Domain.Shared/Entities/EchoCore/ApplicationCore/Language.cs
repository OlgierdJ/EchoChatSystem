using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

public class Language : BaseEntity<uint>
{
    public string Name { get; set; } //English (United States), 普通话, Dansk
    public string LanguageCode { get; set; } //en-US, zh-CN, da-DK
    public ICollection<AccountSettings> AccountSettings { get; set; } //accounts that uses this language
    public ICollection<ServerBot> ServerBots { get; set; } //bots that supports uses this language
}
