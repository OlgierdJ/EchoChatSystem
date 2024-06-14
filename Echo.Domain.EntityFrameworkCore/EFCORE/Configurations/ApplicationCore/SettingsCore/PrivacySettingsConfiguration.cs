using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class PrivacySettingsConfiguration : IEntityTypeConfiguration<PrivacySettings>
{
    public void Configure(EntityTypeBuilder<PrivacySettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.Property(b => b.DMFromFriends).HasConversion<int>().IsRequired();
        builder.Property(b => b.DMFromUnknownUsers).HasConversion<int>().IsRequired();
        builder.Property(b => b.DMFromServerChatroom).HasConversion<int>().IsRequired();
        builder.Property(b => b.DMSpamFilter).HasConversion<int>().IsRequired();
        //builder.HasOne(b => b.Account).WithOne().HasForeignKey<PrivacySettings>(b => b.Id).IsRequired(false);
        builder.HasOne(b => b.AccountSettings).WithOne(e => e.PrivacySettings).HasForeignKey<PrivacySettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
