using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelPermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelPermission>
{
    public void Configure(EntityTypeBuilder<ServerVoiceChannelPermission> builder)
    {
        builder.HasKey(b => new { b.ChannelId, b.PermissionId });

        builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Channel).WithMany(b => b.AllowedPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
    }
}