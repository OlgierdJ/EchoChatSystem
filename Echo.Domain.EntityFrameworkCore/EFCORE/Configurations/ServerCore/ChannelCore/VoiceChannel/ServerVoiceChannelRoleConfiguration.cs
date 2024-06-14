using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelRoleConfiguration : IEntityTypeConfiguration<ServerVoiceChannelRole>
{
    public void Configure(EntityTypeBuilder<ServerVoiceChannelRole> builder)
    {
        builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId });

        builder.HasMany(b => b.Permissions).WithOne(b => b.ChannelRole).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(b => b.Channel).WithMany(b => b.AllowedRoles).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Role).WithMany(b => b.VoiceChannelRoles).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
    }
}