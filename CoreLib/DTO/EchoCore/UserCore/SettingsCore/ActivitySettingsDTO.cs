namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class ActivitySettingsDTO
    {
        public ulong Id { get; set; }
        public bool DisplayCurrentActivityAsAStatusMessage { get; set; }
        public bool ShareActivityStatusOnLargeServerJoin { get; set; }
        public bool AllowFriendsToJoinGame { get; set; }
        public bool AllowVoiceChannelParticipantsToJoinGame { get; set; }
    }
}
