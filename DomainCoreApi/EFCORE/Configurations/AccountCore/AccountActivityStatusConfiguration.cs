using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountActivityStatusConfiguration : IEntityTypeConfiguration<AccountActivityStatus>
    {
        public void Configure(EntityTypeBuilder<AccountActivityStatus> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
            builder.Property(b => b.Description).HasMaxLength(200).IsRequired(false);
            builder.Property(b => b.Icon).HasMaxLength(200).IsRequired();
            builder.Property(b => b.IconColor).HasMaxLength(16).IsRequired();
            builder.HasMany(b => b.Accounts).WithOne(b=>b.ActivityStatus).HasForeignKey(b=>b.ActivityStatusId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
