using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasMany(e => e.Recipients).WithMany(e => e.Roles).UsingEntity<AccountRole>(
                l => l.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId),
                r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId)
                );
            builder.HasMany(b => b.Permissions).WithMany(e => e.Roles).UsingEntity<RolePermission>(

                  l => l.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId),
                r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId)
            );

        }
    }
}
