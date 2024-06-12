using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore;

public class ServerVoiceChannelRolePermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelRolePermission>
{

    public void Configure(EntityTypeBuilder<ServerVoiceChannelRolePermission> builder)
    {
        builder.HasKey(b => new { b.ChannelId, b.RoleId, b.PermissionId });

        builder.Property(b => b.State);

        builder.HasOne(b => b.Permission).WithMany(/*ved ik helt hvad der skal være her*/).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Role).WithMany(b => b.VoiceChannelRolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Channel).WithMany(b => b.RolePermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.ChannelRole).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);
    }
}