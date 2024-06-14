using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelConfiguration : IEntityTypeConfiguration<ServerVoiceChannel>
{
    public void Configure(EntityTypeBuilder<ServerVoiceChannel> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
        builder.Property(b => b.Topic).HasMaxLength(100).IsRequired(false);
        builder.Property(b => b.SlowMode).HasDefaultValue(0).IsRequired();
        builder.Property(b => b.IsAgeRestricted).IsRequired();
        builder.Property(b => b.IsPrivate).IsRequired();
        builder.Property(b => b.BitRate).IsRequired();
        builder.Property(b => b.VideoQuality).HasConversion<int>().IsRequired();
        builder.Property(b => b.UserLimit).HasDefaultValue(0).IsRequired();

        builder.HasOne(b => b.Owner).WithMany(b => b.VoiceChannels).HasForeignKey(b => b.OwnerId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Category).WithMany(b => b.VoiceChannels).HasForeignKey(b => b.CategoryId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Region).WithMany(b => b.VoiceChannels).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasOne(b => b.ServerSettings).WithOne(b => b.InactiveChannel).HasForeignKey<ServerSettings>(b => b.InactiveChannelId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        builder.HasMany(b => b.Muters).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.Invites).WithOne(b => b.VoiceChannel).HasForeignKey(b => b.VoiceChannelId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasMany(b => b.AllowedPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(b => b.AllowedRoles).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelCategoryId, b.RoleId }).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(b => b.RolePermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(b => b.MemberSettings).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(b => b.MemberPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade);
    }
}