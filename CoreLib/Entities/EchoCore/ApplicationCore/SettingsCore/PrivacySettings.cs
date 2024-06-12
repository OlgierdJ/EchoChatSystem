using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings;

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
