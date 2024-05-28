using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelPermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelPermission>
    {
        public void Configure(EntityTypeBuilder<ServerVoiceChannelPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.PermissionId });

            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(b => b.Channel).WithMany(b => b.AllowedPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}