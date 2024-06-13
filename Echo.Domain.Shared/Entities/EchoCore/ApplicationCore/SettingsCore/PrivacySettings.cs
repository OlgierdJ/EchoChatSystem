using Echo.Application.Contracts.Enums;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class PrivacySettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public AccountSettings AccountSettings { get; set; }
    //public ulong AccountId { get; set; }
    public DMAllow DMFromFriends { get; set; }
    public DMAllow DMFromUnknownUsers { get; set; }
    public DMAllow DMFromServerChatroom { get; set; }
    public DMSpamFilter DMSpamFilter { get; set; }
    //public Account? Account { get; set; }
}
