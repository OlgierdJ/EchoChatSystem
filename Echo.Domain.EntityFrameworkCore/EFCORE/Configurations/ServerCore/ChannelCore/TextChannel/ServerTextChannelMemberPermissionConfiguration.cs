using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMemberPermissionConfiguration : IEntityTypeConfiguration<ServerTextChannelMemberPermission>
{

    public void Configure(EntityTypeBuilder<ServerTextChannelMemberPermission> builder)
    {
        builder.HasKey(b => new { b.ChannelId, b.AccountId });

        builder.Property(b => b.State).IsRequired(false);

        builder.HasOne(b => b.MemberSettings).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Channel).WithMany(b => b.MemberPermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Profile).WithMany(b => b.TextChannelMemberPermissions).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Server).WithMany(b => b.TextChannelMemberPermissions).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
