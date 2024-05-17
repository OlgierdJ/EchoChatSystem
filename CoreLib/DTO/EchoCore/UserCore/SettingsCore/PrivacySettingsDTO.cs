using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class PrivacySettingsDTO
    {
        public ulong Id { get; set; }
        public DMAllow DMFromFriends { get; set; }
        public DMAllow DMFromUnknownUsers { get; set; }
        public DMAllow DMFromServerChatroom { get; set; }
        public DMSpamFilter DMSpamFilter { get; set; }
    }
}
