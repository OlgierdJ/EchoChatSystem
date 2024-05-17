using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
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
    public class ServerPermissionConfiguration : IEntityTypeConfiguration<ServerPermission> 
    {
        public void Configure(EntityTypeBuilder<ServerPermission> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b=>b.Name).IsRequired();
            builder.Property(b => b.Description).IsRequired(false);

            builder.HasOne(b => b.Category).WithMany(b => b.Permissions).HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberSettings).WithOne().HasForeignKey(b => new {b.ChannelCategoryId,b.ProfileId}).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.TextChannelMemberSettings).WithOne().HasForeignKey(b => new {b.ChannelId,b.ProfileId}).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.TextChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberSettings).WithOne().HasForeignKey(b => new {b.ChannelId,b.ProfileId}).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
