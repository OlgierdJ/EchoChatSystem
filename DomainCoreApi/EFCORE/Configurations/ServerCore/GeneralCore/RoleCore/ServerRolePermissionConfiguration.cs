﻿using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
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
    public class ServerRolePermissionConfiguration : IEntityTypeConfiguration<ServerRolePermission>
    {
        public void Configure(EntityTypeBuilder<ServerRolePermission> builder)
        {
            builder.HasKey(b => new { b.RoleId, b.PermissionId });

            builder.Property(b => b.State).HasDefaultValue(null).IsRequired(false);

            builder.HasOne(b=>b.Role).WithMany(b=>b.Permissions).HasForeignKey(b=>b.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Permission).WithMany(b => b.RolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
