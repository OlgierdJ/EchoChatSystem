using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class AccessibilitySettings
    {
        //Color stuff
        public byte SaturationPercent { get; set; }
        public bool ApplySaturationToCustomColors { get; set; }
        public bool AlwaysUnderlineLinks { get; set; }
        public bool SyncProfileTheme { get; set; }
        public bool SyncContrastSettings { get; set; }

        public RoleColorMode RoleColorMode { get; set; }

        //Animation stuff
        public bool SyncReducedMotionWithPC { get; set; }
        public bool ReducedMotion { get; set; } //overwrites pc sync
        public bool AutoPlayGIFsOnAppFocus { get; set; }
        public bool PlayAnimatedEmojis { get; set; }

        public StickerAnimationMode StickerAnimationMode { get; set; }

        //Visibility stuff
        public bool ShowSendMessageButton { get; set; }

        //Text to speech stuff
        public bool AllowTextToSpeech { get; set; }
        public byte TextToSpeechRate { get; set; }

        public ulong AccountSettingsId { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
