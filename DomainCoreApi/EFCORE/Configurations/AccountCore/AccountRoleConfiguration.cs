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
            builder.HasOne(x => x.Role).WithMany(e=>e.Recipients).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Account).WithMany(e=>e.Roles).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
