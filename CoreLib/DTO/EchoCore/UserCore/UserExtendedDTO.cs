using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.SettingsCore;
using CoreLib.DTO.EchoCore.SubscriptionCore;
using CoreLib.DTO.EchoCore.ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.EchoCore.MiscCore;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserExtendedDTO //loaded at app startup and lazy loaded properties into it upon navigation.
    {

        public ulong Id { get; set; }
        public string DisplayName { get; set; } //overwritten by accountprofile displayname or nickname if present
        public string Name { get; set; } //unique handle
        public string ImageIconURL { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string BannerColour { get; set; }
        public DateTime MemberSince { get; set; }

        public LanguageDTO? Language { get; set; } //display always
        public AccessibilitySettingsDTO? AccessibilitySettings { get; set; } //display always
        public ActivitySettingsDTO? ActivitySettings { get; set; } //display always
        public AdvancedSettingsDTO? AdvancedSettings { get; set; } //display always
        public AppearanceSettingsDTO? AppearanceSettings { get; set; } //display always
        public BillingInformationDTO? BillingInformation { get; set; } //display always
        public ChatSettingsDTO? ChatSettings { get; set; } //display always 
        public FriendRequestSettingsDTO? FriendRequestSettings { get; set; } //display always 
        public GameOverlaySettingsDTO? GameOverlaySettings { get; set; } //display always 
        public KeybindSettingsDTO? KeybindSettings { get; set; } //display always 
        public NotificationSettingsDTO? NotificationSettings { get; set; } //display always 
        public PrivacySettingsDTO? PrivacySettings { get; set; } //display always 
        public SoundboardSettingsDTO? SoundboardSettings { get; set; } //display always 
        public StreamerModeSettingsDTO? StreamerModeSettings { get; set; } //display always 
        public VideoSettingsDTO? VideoSettings { get; set; } //display always 
        public VoiceSettingsDTO? VoiceSettings { get; set; } //display always 
        public WindowSettingsDTO? WindowSettings { get; set; } //display always 

        public UserProfileDTO? UserProfile { get; set; } //display always
        public ICollection<ServerProfileDTO>? ServerProfiles { get; set; } //display always
        public ICollection<DeviceSessionDTO>? Devices { get; set; } //display always //not implemented at first
        public ICollection<UserConnectionDTO>? Connections { get; set; } //display always //not implemented at first.

        public ICollection<ServerFolderDTO>? ServerFolders { get; set; } //display always
        public ICollection<ServerDTO>? Servers { get; set; } //display always

        public ICollection<UserDTO>? Friends { get; set; } //display always
        public ICollection<FriendRequestDTO>? Requests { get; set; } //display always
        public ICollection<BlockedUserDTO>? BlockedUsers { get; set; } //display always
        public ICollection<UserDTO>? Suggestions { get; set; } //display always
        public ICollection<ServerRoleDTO>? Roles { get; set; } //only display if on server.
        public ICollection<PermissionDTO>? AppPermissions { get; set; } //only display if on server.

        //public ICollection<ApplicationRoleDTO>? AppRoles { get; set; } //maybe not needed
        //public bool EmailConfirmed { get; set; } dont know if client needs this
        //public bool PhoneNumberConfirmed { get; set; } dont know if client needs this
        //public DateTime DateOfBirth { get; set; } dont know if client needs this
    }
}
