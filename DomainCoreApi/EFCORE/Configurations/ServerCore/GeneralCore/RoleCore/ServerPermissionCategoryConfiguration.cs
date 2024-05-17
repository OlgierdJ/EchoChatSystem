using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionCategoryConfiguration : IEntityTypeConfiguration<ServerPermissionCategory>
    {
        public void Configure(EntityTypeBuilder<ServerPermissionCategory> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b=>b.Description).IsRequired(false);

            builder.HasMany(b => b.Permissions).WithOne(b=>b.Category).HasForeignKey(b=>b.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
