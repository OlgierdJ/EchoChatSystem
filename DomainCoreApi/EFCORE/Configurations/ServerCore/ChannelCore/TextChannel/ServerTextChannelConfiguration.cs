using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
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
            builder.Property(b => b.Topic).IsRequired(false);
            builder.Property(b => b.SlowMode).HasDefaultValue(0).IsRequired();
            builder.Property(b => b.IsAgeRestricted).IsRequired();
            builder.Property(b => b.IsPrivate).IsRequired();

            builder.HasOne(b => b.ServerSettings).WithMany().HasForeignKey(b => b.Id).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Pinboard).WithOne(b => b.Owner).HasForeignKey<ServerTextChannelPinboard>(b => b.Id).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(b => b.Webhooks).WithOne(b => b.TextChannel).HasForeignKey(b => b.TextChannelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.Messages).WithOne(b => b.MessageHolder).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(b => b.MessageTrackers).WithOne(b => b.CoOwner).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(b => b.Invites).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(b => b.Muters).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(b => b.AllowedPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(b => b.Category).WithMany(b => b.TextChannels).HasForeignKey(b => b.CategoryId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(b => b.Owner).WithMany(b => b.TextChannels).HasForeignKey(b => b.OwnerId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(b => b.RoleSettings).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelCategoryId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.RoleId }).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(b => b.MemberSettings).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasMany(b => b.MemberPermissions).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
