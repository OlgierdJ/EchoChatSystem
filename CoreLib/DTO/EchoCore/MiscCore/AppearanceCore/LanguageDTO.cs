using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.AppearanceCore
{

    public class LanguageDTO : ILanguage
    {
        public uint Id { get; set; }
        public string Name { get; set; } //English (United States), 普通话, Dansk
        public string LanguageCode { get; set; } //en-US, zh-CN, da-DK
    }
}
