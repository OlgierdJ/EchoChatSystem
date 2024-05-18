using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelPermissionConfiguration : IEntityTypeConfiguration<ServerTextChannelPermission>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.PermissionId });

            builder.HasOne(b => b.Channel).WithMany(b => b.AllowedPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}