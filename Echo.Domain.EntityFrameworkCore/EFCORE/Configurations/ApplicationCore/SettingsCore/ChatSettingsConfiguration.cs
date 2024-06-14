using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class ChatSettingsConfiguration : IEntityTypeConfiguration<ChatSettings>
{
    public void Configure(EntityTypeBuilder<ChatSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.Property(b => b.DisplayVideosAndImagesFromLink).IsRequired();
        builder.Property(b => b.DisplayDirectVideosAndImagesUploads).IsRequired();
        builder.Property(b => b.LoadImageDescriptionsWithImages).IsRequired();
        builder.Property(b => b.PreviewEmbedsAndWebsiteLinks).IsRequired();
        builder.Property(b => b.ShowEmoteReactionsOnMessages).IsRequired();
        builder.Property(b => b.AutoConvertEmoticonsToEmojis).IsRequired();
        builder.Property(b => b.EnableStickerSuggestions).IsRequired();
        builder.Property(b => b.StickersInAutocomplete).IsRequired();
        builder.Property(b => b.PreviewEmojisAndMarkdownWhilstTyping).IsRequired();
        builder.Property(b => b.OpenThreadsInSplitView).IsRequired();
        builder.Property(b => b.ContentSpoilerMode).HasConversion<int>().IsRequired();

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.ChatSettings).HasForeignKey<ChatSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
