using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountServerFolderConfiguration : IEntityTypeConfiguration<AccountServerFolder>
    {
        public void Configure(EntityTypeBuilder<AccountServerFolder> builder)
        {
            builder.HasKey(b => new { b.Id, b.Name });
            builder.Property(b=>b.DisplayName).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.AvatarFileURL).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.BannerColor).HasMaxLength(18).IsRequired();
            builder.Property(b=>b.About).HasMaxLength(256).IsRequired(false);
            builder.HasOne(b => b.Account).WithOne(b => b.Profile).HasForeignKey<AccountServerFolder>(b=>b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
