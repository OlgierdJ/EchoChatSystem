using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.Category;

public class ServerChannelCategoryRolePermissionConfiguration : IEntityTypeConfiguration<ServerChannelCategoryRolePermission>
{

    public void Configure(EntityTypeBuilder<ServerChannelCategoryRolePermission> builder)
    {
        builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId, b.PermissionId });

        builder.Property(b => b.State);

        builder.HasOne(b => b.ChannelCategoryRole).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelCategoryId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Role).WithMany(b => b.ChannelCategoryRolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
        // maybe a one to one
        builder.HasOne(b => b.ChannelCategory).WithMany().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
    }
}