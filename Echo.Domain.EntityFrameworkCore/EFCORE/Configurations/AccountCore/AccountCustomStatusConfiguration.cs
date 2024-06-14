using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountCustomStatusConfiguration : IEntityTypeConfiguration<AccountCustomStatus>
{
    public void Configure(EntityTypeBuilder<AccountCustomStatus> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CustomMessage).HasMaxLength(255).IsRequired();
        builder.Property(b => b.ExpirationTime).IsRequired(false);
        builder.HasOne(b => b.Account).WithOne(b => b.CustomStatus).HasForeignKey<AccountCustomStatus>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
