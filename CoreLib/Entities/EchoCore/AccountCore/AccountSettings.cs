using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountSettings : BaseEntity<ulong>
    {
        //public ulong AccountId { get; set; }
        public uint LanguageId { get; set; }
        public Account  Account { get; set; }


        //Application Settings
        public AccessibilitySettings AccessibilitySettings { get; set; } //should be created on account creation
        public AppearanceSettings AppearanceSettings { get; set; } //should be created on account creation
        public AdvancedSettings AdvancedSettings { get; set; } //should be created on account creation
        //Payment settings
        //maybe add sonar section?
        //Where to put subscriptions section?
        //gifts? why in paymentsettings?
        public BillingInformation BillingInformation { get; set; } //should be created on account creation
        public ChatSettings ChatSettings { get; set; } //should be created on account creation
        public FriendRequestSettings FriendRequestSettings { get; set; } //should be created on account creation
        public KeybindSettings KeybindSettings { get; set; } //Only if we make desktop app cause browsers have varied support //should be created on account creation
        public Language Language { get; set; }//should be created on account creation
        public NotificationSettings NotificationSettings { get; set; } //should be created on account creation
        public PrivacySettings PrivacySettings { get; set; }//should be created on account creation
        public SoundboardSettings SoundboardSettings { get; set; }//should be created on account creation
        public StreamerModeSettings StreamerModeSettings { get; set; }//should be created on account creation
        public VoiceSettings VoiceSettings { get; set; }//should be created on account creation
        public VideoSettings VideoSettings { get; set; }//should be created on account creation
        public ActivitySettings ActivitySettings { get; set; }//should be created on account creation



    }
}
