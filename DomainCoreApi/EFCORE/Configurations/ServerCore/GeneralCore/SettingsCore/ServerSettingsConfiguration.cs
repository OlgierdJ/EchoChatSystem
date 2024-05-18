using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerSettingsConfiguration : IEntityTypeConfiguration<ServerSettings>
    {
        public void Configure(EntityTypeBuilder<ServerSettings> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.SendRandomWelcomeMessageWhenSomeoneJoins).IsRequired();
            builder.Property(b => b.PromptMembersToReplyWithASticker).IsRequired();
            builder.Property(b => b.SendHelpfulTipsForServerSetup).IsRequired();
            builder.Property(b => b.DefaultNotificationSettingsMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.ServerImageUrl).IsRequired();
            builder.Property(b => b.ServerInviteBackgroundUrl).IsRequired();
            builder.Property(b => b.ShowMembersInChannelList).IsRequired();
            builder.Property(b => b.VerificationLevelMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.Require2FAForModeratorActions).IsRequired();
            builder.Property(b => b.ExplicitImageFilterMode).HasConversion<int>().IsRequired();

            builder.HasOne(b => b.Server).WithOne(b => b.Settings).HasForeignKey<ServerSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(b => b.InactiveChannel).WithOne(b => b.ServerSettings).HasForeignKey<ServerSettings>(b => b.Id).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(b => b.SystemMessagesChannel).WithOne(b => b.ServerSettings).HasForeignKey<ServerSettings>(b => b.Id).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(b => b.Region).WithMany(b => b.ServerSettings).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }

}
