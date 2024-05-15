using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelRolePermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelRolePermission>
    //Used for displaying state of serverpermission for a specific channel for a specific role
    {
        //combine foreign key channel and role.
        //pk is combination of channel, role and permission
        public ulong ChannelId { get; set; }
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerVoiceChannelRoleConfiguration ChannelRole { get; set; } //cascade
        public ServerVoiceChannelConfiguration Channel { get; set; } //ignore
        public ServerRole Role { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade

        public void Configure(EntityTypeBuilder<ServerVoiceChannelRolePermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.RoleId, b.PermissionId });

            builder.Property(b => b.State);

            builder.HasOne(b => b.Permission).WithMany(/*ved ik helt hvad der skal være her*/).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Role).WithMany(b => b.VoiceChannelRolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Channel).WithMany(b => b.RolePermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.ChannelRole).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}