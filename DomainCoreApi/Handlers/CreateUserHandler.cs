using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.Handlers
{
    public class CreateUserHandler
    {

        public async Task<(User, Account)> CreateHandler(RegisterRequestDTO input)
        {

            var user = await CreateUser(input.Email, input.DateOfBirth);
            var acc = await CreateAccount(input.Username, input.DisplayName);
            return (user, acc);
        }

        public async Task<Account> CreateAccount(string Username, string? DisplayName)
        {
            Account account = new Account()
            {
                Name = Username,
                TimeCreated = DateTime.UtcNow,
                ActivityStatusId = 1,
                CustomStatus = new()
                {
                    CustomMessage = "Online"
                },
                Profile = new()
                {
                    DisplayName = !String.IsNullOrEmpty(DisplayName) ? DisplayName : Username,
                    AvatarFileURL = GetIconFromList(),
                    BannerColor = "Success",
                },
                Settings = new()
                {
                    LanguageId = 10,
                    AccessibilitySettings = new AccessibilitySettings()
                    {
                        SaturationPercent = 255,
                        ApplySaturationToCustomColors = false,
                        AlwaysUnderlineLinks = true,
                        SyncProfileThemes = true,
                        SyncContrastSettings = true,
                        RoleColorMode = RoleColorMode.ShowRoleColorsInNames,
                        SyncReducedMotionWithPC = true,
                        ReducedMotion = true,
                        AutoPlayGIFsOnAppFocus = true,
                        PlayAnimatedEmojis = true,
                        StickerAnimationMode = StickerAnimationMode.AlwaysAnimate,
                        ShowSendMessageButton = true,
                        AllowTextToSpeech = false,
                        TextToSpeechRate = 255,
                    },
                    AppearanceSettings = new AppearanceSettings()
                    {
                        ThemeId = 1,
                        InAppIcon = "",
                        DarkSideBar = true,
                        PixelChatFontScale = 255,
                        PixelSpaceBetweenMessageGroupsScale = 255,
                    },
                    AdvancedSettings = new AdvancedSettings()
                    {
                        DeveloperMode = false,
                        AutoNavigateServerHome = false,
                    },
                    BillingInformation = new BillingInformation(),
                    ChatSettings = new ChatSettings(),
                    FriendRequestSettings = new FriendRequestSettings()
                    {
                        Everyone = true,
                        FriendsOfFriends = false,
                        ServerMembers = false,
                    },
                    KeybindSettings = new KeybindSettings(),
                    NotificationSettings = new NotificationSettings()
                    {
                        FocusModeEnabled = false,
                        EnableDesktopNotifications = true,
                        EnableUnreadMessageBadge = true,
                        EnableTaskbarFlashing = true,
                    },
                    PrivacySettings = new PrivacySettings()
                    {
                        DMFromFriends = DMAllow.Show,
                        DMFromUnknownUsers = DMAllow.Show,
                        DMFromServerChatroom = DMAllow.Show,
                    },
                    SoundboardSettings = new SoundboardSettings()
                    {
                        SoundboardVolume = 100,
                        //Soundboard = 100,
                    },
                    StreamerModeSettings = new StreamerModeSettings()
                    {
                        EnableStreamerMode = false,
                        HidePersonalInformation = true,
                        HideInviteLinks = false,
                        DisableNotifications = false,
                        DisableSounds = false,
                    },
                    VoiceSettings = new VoiceSettings()
                    {
                        InputDevice = "",
                        OutputDevice = "",
                        InputVolume = 100,
                        OutputVolume = 100,
                        InputMode = InputMode.VoiceActivity,
                        EchoCancellation = true,
                        NoiseSuppression = NoiseSuppression.Standard,
                        AdvancedVoiceActivity = false,
                        AutomaticGainControl = false,
                    },
                    VideoSettings = new VideoSettings()
                    {
                        AlwaysPreviewVideo = false,
                        CameraDevice = "",
                        VideoBackground ="",
                        UseOpenH264VideoCodec =false,
                        EnableHardwareAccelerationForVideo =false,
                        EnableForceQualityOfServicePacketPrio = false,
                        UseDDLInjectionToCaptureScreen = false
                    },
                    ActivitySettings = new ActivitySettings()
                    {
                        DisplayCurrentActivityAsAStatusMessage = true,
                        ShareActivityStatusOnLargeServerJoin = true,
                        AllowFriendsToJoinGame = true,
                        AllowVoiceChannelParticipantsToJoinGame = true,
                    }
                },
            };
            return account;
        }

        public async Task<User> CreateUser(string Email, DateTime DateOfBirth)
        {
            User user = new User()
            {

                Email = Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                PasswordSetDate = DateTime.UtcNow,
                DateOfBirth = DateOfBirth,
            };
            return user;
        }
        public async Task CreateAccountProfile()
        {

        }

        public string GetIconFromList()
        {
            List<string> list = new List<string>();
            Random rnd = new Random();
            list.Add("https://imgur.com/qp6Jjpj.png");
            list.Add("https://imgur.com/4eWTjzg.png");
            list.Add("https://imgur.com/thNssAf.png");
            list.Add("https://imgur.com/qMgDaU5.png");
            list.Add("https://imgur.com/Q7pl0t3.png");
            list.Add("https://imgur.com/IkANdGw.png");
            list.Add("https://imgur.com/vmWZgGu.png");
            int r = rnd.Next(list.Count);
            return ((string)list[r]);
        }
    }
}
