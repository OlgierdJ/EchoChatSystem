using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasMany(b => b.Roles).WithMany(e => e.Permissions).UsingEntity<RolePermission>(
            
                l => l.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId),
                  r => r.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId)
            );
        }
    }
}
