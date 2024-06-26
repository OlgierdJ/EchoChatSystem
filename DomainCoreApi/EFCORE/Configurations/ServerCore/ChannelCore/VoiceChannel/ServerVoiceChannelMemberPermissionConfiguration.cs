﻿using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberPermissionConfiguration : IEntityTypeConfiguration<ServerVoiceChannelMemberPermission>
    {
        public void Configure(EntityTypeBuilder<ServerVoiceChannelMemberPermission> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.AccountId, b.PermissionId });

            builder.Property(b => b.State);

            builder.HasOne(b => b.Permission).WithMany(b => b.VoiceChannelMemberPermissions).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(b => b.Profile).WithMany(b => b.VoiceChannelMemberPermissions).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Server).WithMany(b => b.VoiceChannelMemberPermissions).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Channel).WithMany(b => b.MemberPermissions).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.MemberSettings).WithMany(b => b.Permissions).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
