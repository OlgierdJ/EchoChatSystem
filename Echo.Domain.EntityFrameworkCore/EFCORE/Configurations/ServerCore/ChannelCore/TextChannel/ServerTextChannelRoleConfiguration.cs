using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelRoleConfiguration : IEntityTypeConfiguration<ServerTextChannelRole>
{
    public void Configure(EntityTypeBuilder<ServerTextChannelRole> builder)
    {
        builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId });

        builder.HasOne(b => b.Channel).WithMany(b => b.RoleSettings).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Role).WithMany(b => b.TextChannelRoles).HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.Permissions).WithOne(b => b.ChannelRole).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
    }
}
