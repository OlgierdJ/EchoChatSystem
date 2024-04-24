using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
  
    //public class RoleConfiguration : IEntityTypeConfiguration<Role>
    //{
    //    public void Configure(EntityTypeBuilder<Role> builder)
    //    {
    //        builder.HasKey(b => b.Id);

    //        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

    //        builder.HasMany(e => e.Recipients).WithMany(e => e.Roles).UsingEntity<AccountRole>(j =>
    //        {
    //            j.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId);
    //            j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
    //        });
    //        //builder.HasMany(e => e.Permissions).WithMany(e => e.Roles).UsingEntity<RolePermission>();

    //    }
    //}
}
