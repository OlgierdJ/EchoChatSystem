using CoreLib.Entities.Base;
using CoreLib.DTO.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountSettingsDTO //: BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public uint LanguageId { get; set; } 

        //Application Settings
        public AccessibilitySettingsDTO AccessibilitySettings { get; set; }
        public AppearanceSettingsDTO AppearanceSettings { get; set; }
        public AdvancedSettingsDTO AdvancedSettings { get; set; }
        //Payment settings
        //maybe add sonar section?
        //Where to put subscriptions section?
        //gifts? why in paymentsettings?
        public BillingInformationDTO BillingInformation { get; set; }
        public ChatSettingsDTO ChatSettings { get; set; }
        public FriendRequestSettingsDTO FriendRequestSettings { get; set; }
        public KeybindSettingsDTO KeybindSettings { get; set; } //Only if we make desktop app cause browsers have varied support
        public LanguageDTO Language { get; set; }
        public NotificationSettingsDTO NotificationSettings { get; set; }
        public PrivacySettingsDTO PrivacySettings { get; set; }
        public SoundboardSettingsDTO SoundboardSettings { get; set; }
        public StreamerModeSettingsDTO StreamerModeSettings { get; set; }
        public VoiceSettingsDTO VoiceSettings { get; set; }
        public VideoSettingsDTO VideoSettings { get; set; }
        public ActivitySettingsDTO ActivitySettings { get; set; }
    }
}
