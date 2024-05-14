using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    //need review
    public class ServerChannelCategoryPermissionConfiguration : IEntityTypeConfiguration<ServerChannelCategoryPermission>
    //used for mapping displayed permissions within a channelcategory
    {
        public void Configure(EntityTypeBuilder<ServerChannelCategoryPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelCategoryId, b.PermissionId });

            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.ChannelCategory).WithMany(b=>b.AllowedPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
