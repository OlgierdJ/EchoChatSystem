﻿using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using CoreLib.Models;

namespace DomainCoreApi.Handlers
{
    public class CreateUserHandler
    {

        public async Task<(User,Account)> CreateHandler(RegisterUserModel input)
        {

            var us = await CreateUser(input.Email, input.DateOfBirth);
            var acc = await CreateAccount(input.Username, input.DisplayName);
            return (us, acc);
        }

        public async Task<Account> CreateAccount(string Username,string? DisplayName)
        {
            Account account = new Account()
            {
                Name = Username,
                TimeCreated = DateTime.UtcNow,
                ActivityStatus = new()
                {
                    Name = "Online",
                    Icon = "Icons.Material.Filled.Circle",
                    IconColor = "Success"
                },
                Profile = new()
                {
                    DisplayName = !String.IsNullOrEmpty(DisplayName) ? DisplayName : Username,
                    AvatarFileURL = GetIconFromList(),
                    BannerColor = "Success",
                },
                Settings = new()
                {
                    AccessibilitySettings = new AccessibilitySettings()
                    {
                        SaturationPercent = 255,
                        ApplySaturationToCustomColors = false,
                        AlwaysUnderlineLinks = true,
                        SyncProfileTheme = true,
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
                        PixelGroupSpaceScale = 255,
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
                    Language = new Language()
                    {
                        Name = "English (United States)",
                        LanguageCode = "en-US",
                    },
                    NotificationSettings = new NotificationSettings()
                    {
                        FocusModeEnabled = false,
                        DesktopNotification = true,
                        UnreadMessageBadge = true,
                        TaskbarFlashing = true,
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
                        Soundboard = 100,
                    },
                    StreamerModeSettings = new StreamerModeSettings()
                    {
                        StreamerMode = false,
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
                    VideoSettings = new VideoSettings(),
                    ActivitySettings = new ActivitySettings()
                },
            };
            return account;
        }

        public async Task<User> CreateUser(string Email,DateTime DateOfBirth)
        {
            // user \\
            /* Email  DateOfBirth */
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