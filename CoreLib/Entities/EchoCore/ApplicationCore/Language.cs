using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class Language : BaseEntity<uint>
    {
        public string Name { get; set; } //English (United States), 普通话, Dansk
        public string LanguageCode { get; set; } //en-US, zh-CN, da-DK
        public ICollection<AccountSettings> AccountSettings { get; set; } //accounts that uses this language
        public ICollection<ServerBot> ServerBots { get; set; } //bots that supports uses this language
    }
}
