using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class ActivitySettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public bool DisplayCurrentActivityAsAStatusMessage { get; set; }
    public bool ShareActivityStatusOnLargeServerJoin { get; set; }
    public bool AllowFriendsToJoinGame { get; set; }
    public bool AllowVoiceChannelParticipantsToJoinGame { get; set; }
    public AccountSettings AccountSettings { get; set; }
}
