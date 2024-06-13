using Echo.Application.Contracts.Enums;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

public class ServerSettings : BaseEntity<ulong>
{
    //pause invites serverwide, pause dms serverwide
    //General settings
    public uint RegionId { get; set; }
    public ulong? InactiveChannelId { get; set; } //only voice channel
    public ulong? SystemMessagesChannelId { get; set; } //only text channel
    public bool SendRandomWelcomeMessageWhenSomeoneJoins { get; set; }
    public bool PromptMembersToReplyWithASticker { get; set; }
    public bool SendHelpfulTipsForServerSetup { get; set; }
    public DefaultNotificationSettings DefaultNotificationSettingsMode { get; set; }
    public string ServerImageUrl { get; set; }
    //public string InactiveTimeOut { get; set; }
    //public string ServerInviteBackgroundUrl { get; set; }
    //Safety Settings
    public bool ShowMembersInChannelList { get; set; }
    public VerificationLevel VerificationLevelMode { get; set; }
    public bool Require2FAForModeratorActions { get; set; }
    public ExplicitImageFilter ExplicitImageFilterMode { get; set; }

    public Server Server { get; set; } //mapped through id.
    public ServerVoiceChannel? InactiveChannel { get; set; }
    public ServerTextChannel? SystemMessagesChannel { get; set; }
    public ServerRegion Region { get; set; }


    public ICollection<ServerBotIntegration>? Integrations { get; set; } //maybe put this into settings

}
