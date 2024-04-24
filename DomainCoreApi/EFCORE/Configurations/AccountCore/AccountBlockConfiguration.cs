using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountBlockConfiguration : IEntityTypeConfiguration<AccountBlock>
    {
        public void Configure(EntityTypeBuilder<AccountBlock> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b=>b.TimeBlocked).ValueGeneratedOnAdd();
            builder.HasOne(b => b.Blocker).WithMany(e=>e.BlockedAccounts).HasForeignKey(b=>b.BlockerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Blocked).WithMany().HasForeignKey(b=>b.BlockedId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
