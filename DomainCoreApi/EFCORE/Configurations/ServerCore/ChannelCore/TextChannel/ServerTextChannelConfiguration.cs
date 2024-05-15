using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelConfiguration : IEntityTypeConfiguration<ServerTextChannel>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannel> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b=>b.Topic).IsRequired(false);
            builder.Property(b => b.SlowMode).HasDefaultValue(0).IsRequired(false);
            builder.Property(b => b.IsAgeRestricted).IsRequired();
            builder.Property(b => b.IsPrivate).IsRequired();

            builder.HasOne(b => b.ServerSettings).WithMany().HasForeignKey(b => b.Id).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(b => b.Pinboard).WithOne(b => b.Owner).HasForeignKey<ServerTextChannelPinboard>(b => b.OwnerId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasMany(b=>b.Webhooks).WithOne(b=>b.TextChannel).HasForeignKey(b=>b.TextChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.Messages).WithOne(b => b.MessageHolder).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.MessageTrackers).WithOne(b => b.CoOwner).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.Invites).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasMany(b => b.Muters).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasMany(b => b.AllowedPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

            builder.HasMany(b => b.AllowedRoles).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelCategoryId, b.RoleId }).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasMany(b => b.MemberSettings).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.MemberPermissions).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        }
    }
}
