﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class AccessibilitySettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        //Color stuff
        public byte SaturationPercent { get; set; } //max 100
        public bool ApplySaturationToCustomColors { get; set; }
        public bool AlwaysUnderlineLinks { get; set; }
        public RoleColorMode RoleColorMode { get; set; }

        public bool SyncProfileThemes { get; set; } //sync profiles with echo theme
        public bool SyncContrastSettings { get; set; } //allows echo to use pc contrast theme


        //Animation stuff
        public bool SyncReducedMotionWithPC { get; set; }
        public bool ReducedMotion { get; set; } //overwrites pc sync
        public bool AutoPlayGIFsOnAppFocus { get; set; }
        public bool PlayAnimatedEmojis { get; set; }

        public StickerAnimationMode StickerAnimationMode { get; set; }

        //Visibility stuff
        public bool ShowSendMessageButton { get; set; }
        public bool UseLegacyChatInput { get; set; }

        //Text to speech stuff
        public bool AllowTextToSpeech { get; set; }
        public byte TextToSpeechRate { get; set; } //default 4 (4x0.25) max 40 aka 10x

        public AccountSettings AccountSettings { get; set; }
    }
}
