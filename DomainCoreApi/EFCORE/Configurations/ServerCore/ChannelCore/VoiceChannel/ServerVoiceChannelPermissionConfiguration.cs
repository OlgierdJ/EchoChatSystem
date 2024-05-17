using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelPermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelPermission>
    //used for mapping displayed permissions within a channel
    {
        //pk is combination of channel, and permission
        public ulong ChannelId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerVoiceChannel Channel { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade

        public void Configure(EntityTypeBuilder<ServerVoiceChannelPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.PermissionId });

            builder.HasOne(b => b.Permission).WithMany().HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Channel).WithMany(b => b.AllowedPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}