using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Role).WithMany(/*b=>b.Recipients*/).HasForeignKey(b=>b.RoleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Account).WithMany(b=>b.Roles).HasForeignKey(b=>b.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
