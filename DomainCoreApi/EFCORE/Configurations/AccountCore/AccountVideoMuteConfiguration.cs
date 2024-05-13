using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountVideoMuteConfiguration : IEntityTypeConfiguration<AccountProfile>
    {
        public void Configure(EntityTypeBuilder<AccountProfile> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b=>b.DisplayName).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.AvatarFileURL).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.BannerColor).HasMaxLength(18).IsRequired();
            builder.Property(b=>b.About).HasMaxLength(256).IsRequired(false);
            builder.HasOne(b => b.Account).WithOne(b => b.Profile).HasForeignKey<AccountProfile>(b=>b.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
