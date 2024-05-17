using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class ChatSettingsDTO
    {
        public ulong Id { get; set; }
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
    }
}
