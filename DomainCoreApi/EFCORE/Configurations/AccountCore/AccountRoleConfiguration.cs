using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder
                .HasKey(b => new { b.RoleId, b.AccountId });
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Account).WithMany().HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
