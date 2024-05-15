using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryMemberPermissionConfiguration : IEntityTypeConfiguration<ServerChannelCategoryMemberPermission>
    {
        public void Configure(EntityTypeBuilder<ServerChannelCategoryMemberPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelCategoryId, b.ProfileId, b.PermissionId });

            builder.Property(b => b.State);

            builder.HasOne(b => b.Permission).WithMany(b => b.CategoryMemberPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Profile).WithMany(b => b.CategoryMemberPermissions).HasForeignKey(b => b.ProfileId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.ChannelCategory).WithMany(b => b.MemberPermissions).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.MemberSettings).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelCategoryId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
