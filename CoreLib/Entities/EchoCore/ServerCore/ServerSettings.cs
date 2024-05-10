using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerSettings
    {
        //pause invites serverwide, pause dms serverwide
        //General settings
        public ulong ServerId { get; set; }
        public Server Server { get; set; }
       // public ServerVoiceChannel InactiveChannel { get; set; }
        public ulong InactiveChannelId { get; set; } //only voice channel
        public ulong SystemMessagesChannelId { get; set; } //only text channel
        public bool SendRandomWelcomeMessageWhenSomeoneJoins {  get; set; }
        public bool PromptMembersToReplyWithASticker {  get; set; }
        public bool SendHelpfulTipsForServerSetup {  get; set; }
        public DefaultNotificationSettingsEnum  DefaultNotificationSettingsMode {  get; set; }
        public string ServerInviteBackgroundUrl { get; set; }
        //Safety Settings
        public bool ShowMembersInChannelList { get; set; }
        public VerificationLevel  VerificationLevelMode { get; set; }
        public bool Require2FAForModeratorActions { get; set; }
        public ExplicitImageFilter  ExplicitImageFilterMode { get; set; }
        public ICollection<ServerAuditLog> AuditLogs { get; set; }
        public ICollection<ServerBan> ServerBanList { get; set; }

    }
    public enum DefaultNotificationSettingsEnum
    {
        AllMessages,
        OnlyAtMentions
    }
    public enum VerificationLevel
    {
        None,
        Low,
        Medium,
        High,
        Highest
    }
    public enum ExplicitImageFilter
    {
        DoNotFilter,
        FilterFromAll,
        FilterFromMembersWithoutRoles,
    }
}
