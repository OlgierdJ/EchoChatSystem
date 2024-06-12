using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore;

public class ServerTextChannelRolePermissionConfiguration : IEntityTypeConfiguration<ServerTextChannelRolePermission>
{
    public void Configure(EntityTypeBuilder<ServerTextChannelRolePermission> builder)
    {
        builder.HasKey(b => new { b.ChannelId, b.RoleId });

        builder.Property(b => b.State).IsRequired(false);

        builder.HasOne(b => b.ChannelRole).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Channel).WithMany(b => b.RolePermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Role).WithMany(b => b.TextChannelRolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade); ;
    }
}