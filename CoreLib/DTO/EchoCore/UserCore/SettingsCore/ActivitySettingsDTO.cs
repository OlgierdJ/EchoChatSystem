namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IActivitySettings
{
    bool AllowFriendsToJoinGame { get; set; }
    bool AllowVoiceChannelParticipantsToJoinGame { get; set; }
    bool DisplayCurrentActivityAsAStatusMessage { get; set; }
    ulong Id { get; set; }
    bool ShareActivityStatusOnLargeServerJoin { get; set; }
}

public class ActivitySettingsDTO : IActivitySettings
{
    public ulong Id { get; set; }
    public bool DisplayCurrentActivityAsAStatusMessage { get; set; }
    public bool ShareActivityStatusOnLargeServerJoin { get; set; }
    public bool AllowFriendsToJoinGame { get; set; }
    public bool AllowVoiceChannelParticipantsToJoinGame { get; set; }
}
