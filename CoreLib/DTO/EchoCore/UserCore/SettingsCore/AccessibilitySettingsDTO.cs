using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public interface IAccessibilitySettings
    {
        bool AllowTextToSpeech { get; set; }
        bool AlwaysUnderlineLinks { get; set; }
        bool ApplySaturationToCustomColors { get; set; }
        bool AutoPlayGIFsOnAppFocus { get; set; }
        ulong Id { get; set; }
        bool PlayAnimatedEmojis { get; set; }
        bool ReducedMotion { get; set; }
        RoleColorMode RoleColorMode { get; set; }
        byte SaturationPercent { get; set; }
        bool ShowSendMessageButton { get; set; }
        StickerAnimationMode StickerAnimationMode { get; set; }
        bool SyncContrastSettings { get; set; }
        bool SyncProfileThemes { get; set; }
        bool SyncReducedMotionWithPC { get; set; }
        byte TextToSpeechRate { get; set; }
        bool UseLegacyChatInput { get; set; }
    }

    public class AccessibilitySettingsDTO : IAccessibilitySettings
    {
        public ulong Id { get; set; }
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
    }
}
