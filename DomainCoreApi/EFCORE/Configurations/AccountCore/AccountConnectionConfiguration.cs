using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountConnectionConfiguration : IEntityTypeConfiguration<AccountConnection>
    {
        public void Configure(EntityTypeBuilder<AccountConnection> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b=>b.ExternalProvider).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.ExternalToken).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.InternalProvider).HasMaxLength(256).IsRequired();
            builder.Property(b=>b.InternalToken).HasMaxLength(256).IsRequired();
            builder.HasOne(b=>b.Account).WithMany(b=>b.Connections).HasForeignKey(b=>b.AccountId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
