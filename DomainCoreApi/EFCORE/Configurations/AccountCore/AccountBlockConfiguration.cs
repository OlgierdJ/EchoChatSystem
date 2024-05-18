using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountBlockConfiguration : IEntityTypeConfiguration<AccountBlock>
    {
        public void Configure(EntityTypeBuilder<AccountBlock> builder)
        {
            builder.HasKey(b => new { b.BlockerId, b.BlockedId });
            builder.Property(b => b.TimeBlocked).HasDefaultValueSql("getdate()");
            builder.HasOne(b => b.Blocker).WithMany(e => e.BlockedAccounts).HasForeignKey(b => b.BlockerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Blocked).WithMany().HasForeignKey(b => b.BlockedId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
