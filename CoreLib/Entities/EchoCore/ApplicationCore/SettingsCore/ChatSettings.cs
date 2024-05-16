using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class ChatSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool DisplayVideosAndImagesFromLink { get; set; }
        public bool DisplayDirectVideosAndImagesUploads { get; set; }
        public bool LoadImageDescriptionsWithImages { get; set; }
        public bool PreviewEmbedsAndWebsiteLinks { get; set; }
        public bool ShowEmoteReactionsOnMessages { get; set; }
        public bool AutoConvertEmoticonsToEmojis { get; set; }
        public bool EnableStickerSuggestions { get; set; }
        public bool StickersInAutocomplete { get; set; }
        public bool PreviewEmojisAndMarkdownWhilstTyping { get; set; }
        public bool OpenThreadsInSplitView { get; set; }
        public ContentSpoilerMode ContentSpoilerMode { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
