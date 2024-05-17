﻿using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.ChatCore.VoiceCore;
using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerSettingsDTO
    {
        public ulong Id { get; set; }
        //pause invites serverwide, pause dms serverwide
        //General settings
        //public uint RegionId { get; set; }
        //public ulong? InactiveChannelId { get; set; } //only voice channel
        //public ulong? SystemMessagesChannelId { get; set; } //only text channel
        public bool SendRandomWelcomeMessageWhenSomeoneJoins { get; set; }
        public bool PromptMembersToReplyWithASticker { get; set; }
        public bool SendHelpfulTipsForServerSetup { get; set; }
        public DefaultNotificationSettingsEnum DefaultNotificationSettingsMode { get; set; }
        public string ServerImageUrl { get; set; }
        public string ServerInviteBackgroundUrl { get; set; }
        //Safety Settings
        public bool ShowMembersInChannelList { get; set; }
        public VerificationLevel VerificationLevelMode { get; set; }
        public bool Require2FAForModeratorActions { get; set; }
        public ExplicitImageFilter ExplicitImageFilterMode { get; set; }
        public VoiceChatDTO? InactiveChannel { get; set; }
        public ChatDTO? SystemMessagesChannel { get; set; }
        public RegionDTO Region { get; set; }


        //public ICollection<ServerBotIntegration>? Integrations { get; set; } //maybe put this into settings
    }
}