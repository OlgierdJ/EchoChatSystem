using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IPrivacySettings
{
    DMAllow DMFromFriends { get; set; }
    DMAllow DMFromServerChatroom { get; set; }
    DMAllow DMFromUnknownUsers { get; set; }
    DMSpamFilter DMSpamFilter { get; set; }
    ulong Id { get; set; }
}

public class PrivacySettingsDTO : IPrivacySettings
{
    public ulong Id { get; set; }
    public DMAllow DMFromFriends { get; set; }
    public DMAllow DMFromUnknownUsers { get; set; }
    public DMAllow DMFromServerChatroom { get; set; }
    public DMSpamFilter DMSpamFilter { get; set; }
}
