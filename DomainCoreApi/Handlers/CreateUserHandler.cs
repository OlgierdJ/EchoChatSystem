using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.UserCore;
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
            // Account \\
            /* Username */
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
                // AccountProfile \\
                /* displayname?  */
                Profile = new()
                {
                    DisplayName = !String.IsNullOrEmpty(DisplayName) ? DisplayName : Username,
                    AvatarFileURL = GetIconFromList(),
                    BannerColor = "Success",

                },
                Settings = new()
                {
                    AccessibilitySettings = new AccessibilitySettings(),
                    AppearanceSettings = new AppearanceSettings()
                    {

                    },
                    AdvancedSettings = new AdvancedSettings(),
                    BillingInformation = new BillingInformation(),
                    ChatSettings = new ChatSettings(),
                    FriendRequestSettings = new FriendRequestSettings(),
                    KeybindSettings = new KeybindSettings(),
                    Language = new Language()
                    {
                        Name= "English (United States)",
                        LanguageCode = "en-US",
                    },
                    NotificationSettings = new NotificationSettings(),
                    PrivacySettings = new PrivacySettings(),
                    SoundboardSettings = new SoundboardSettings(),
                    StreamerModeSettings = new StreamerModeSettings(),
                    VoiceSettings = new VoiceSettings(),
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
