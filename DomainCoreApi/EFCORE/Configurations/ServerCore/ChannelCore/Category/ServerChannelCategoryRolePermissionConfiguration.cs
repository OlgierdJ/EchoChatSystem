using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryRolePermissionConfiguration : IEntityTypeConfiguration<ServerChannelCategoryRolePermission>
    //Used for displaying state of serverpermission for a specific channelcategory for a specific role
    {
        //combine foreign key category and role.
        //pk is combination of category, role and permission
        //public ulong ChannelCategoryId { get; set; }
        //public ulong RoleId { get; set; }
        //public ulong PermissionId { get; set; }
        //public bool? State { get; set; } //true = enabled, null = default, false = disabled
        //public ServerChannelCategoryRole ChannelCategoryRole { get; set; } //cascade
        //public ServerChannelCategory ChannelCategory { get; set; } //ignore
        //public ServerRole Role { get; set; } //ignore
        //public ServerPermission Permission { get; set; } //cascade

        public void Configure(EntityTypeBuilder<ServerChannelCategoryRolePermission> builder)
        {
            builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId, b.PermissionId });

            builder.Property(b => b.State);

            builder.HasOne(b => b.ChannelCategoryRole).WithMany(b => b.Permissions).HasForeignKey(b => new {b.ChannelCategoryId,b.RoleId}).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Role).WithMany(b => b.ChannelCategoryRolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
            // maybe a one to one
            builder.HasOne(b => b.ChannelCategory).WithMany().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}