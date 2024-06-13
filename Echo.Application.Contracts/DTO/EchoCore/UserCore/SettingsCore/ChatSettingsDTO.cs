using Echo.Application.Contracts.Enums;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;

public interface IChatSettings
{
    bool AutoConvertEmoticonsToEmojis { get; set; }
    ContentSpoilerMode ContentSpoilerMode { get; set; }
    bool DisplayDirectVideosAndImagesUploads { get; set; }
    bool DisplayVideosAndImagesFromLink { get; set; }
    bool EnableStickerSuggestions { get; set; }
    ulong Id { get; set; }
    bool LoadImageDescriptionsWithImages { get; set; }
    bool OpenThreadsInSplitView { get; set; }
    bool PreviewEmbedsAndWebsiteLinks { get; set; }
    bool PreviewEmojisAndMarkdownWhilstTyping { get; set; }
    bool ShowEmoteReactionsOnMessages { get; set; }
    bool StickersInAutocomplete { get; set; }
}

public class ChatSettingsDTO : IChatSettings
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
