using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelMemberPermissionConfiguration : IEntityTypeConfiguration<ServerTextChannelMemberPermission>
    {

        public void Configure(EntityTypeBuilder<ServerTextChannelMemberPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.ProfileId });

            builder.Property(b => b.State).IsRequired(false);

            builder.HasOne(b => b.MemberSettings).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Channel).WithMany(b => b.MemberPermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Profile).WithMany(b => b.TextChannelMemberPermissions).HasForeignKey(b => b.ProfileId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
