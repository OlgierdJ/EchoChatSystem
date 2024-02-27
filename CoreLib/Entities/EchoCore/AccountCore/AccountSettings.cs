using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountSettings : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        //Payment settings
        //maybe add sonar section?
        //Where to put subscriptions section?
        //gifts? why in paymentsettings?
        public BillingInformation BillingInformation { get; set; }

        //Application Settings
        public AppearanceSettings AppearanceSettings { get; set; }
        public AccessibilitySettings AccessibilitySettings { get; set; }
        public VoiceSettings    VoiceSettings { get; set; }
        public VideoSettings     VideoSettings { get; set; }
        public SoundboardSettings SoundboardSettings { get; set; }
        public ChatSettings   ChatSettings { get; set; }
        public NotificationSettings  NotificationSettings { get; set; }
        
        public KeybindSettings   KeybindSettings { get; set; } //Only if we make desktop app cause browsers have varied support

        public Language Language { get; set; }
        public StreamerModeSettings StreamerModeSettings { get; set; }
        public AdvancedSettings AdvancedSettings { get; set; }

        //Activity Settings
        public ActivitySettings ActivitySettings { get; set; }
    }
}
