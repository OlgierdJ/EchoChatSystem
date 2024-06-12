using CoreLib.Entities.Enums;

namespace CoreLib.DTO.RequestCore.ServerCore.Server;

public class EditServerRequestDTO
{
    //public ulong Id { get; set; } //get from jwt
    //public ulong ServerId { get; set; } //get from route param
    public uint RegionId { get; set; }
    public ulong? InactiveChannelId { get; set; }
    public ulong? SystemMessagesChannelId { get; set; }
    public string ServerImageURL { get; set; }
    public string Name { get; set; }
    //public string InactiveTimeOut { get; set; } //ignored for now
    public bool SendRandomWelcomeMessageWhenSomeoneJoins { get; set; }
    public bool PromptMembersToReplyWithASticker { get; set; }
    public bool SendHelpfulTipsForServerSetup { get; set; }
    public bool ShowMembersInChannelList { get; set; }
    public bool Require2FAForModeratorActions { get; set; }
    public DefaultNotificationSettings DefaultNotificationSettingsMode { get; set; }
    public VerificationLevel VerificationLevelMode { get; set; }
    public ExplicitImageFilter ExplicitImageFilterMode { get; set; }
}
