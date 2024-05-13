using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    //public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    //{
    //    public void Configure(EntityTypeBuilder<RolePermission> builder)
    //    {
    //        builder
    //            .HasKey(b => new { b.RoleId, b.PermissionId });
    //        builder.HasOne(x => x.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.ClientCascade);
    //        builder.HasOne(x => x.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade); 
          
    //    }
    //}
}
