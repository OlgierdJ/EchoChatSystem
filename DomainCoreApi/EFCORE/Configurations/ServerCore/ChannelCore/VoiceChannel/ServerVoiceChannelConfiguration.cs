using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelConfiguration : IEntityTypeConfiguration<ServerVoiceChannel>
    {
        public uint RegionId { get; set; }
        public uint BitRate { get; set; }
        public VideoQualityMode VideoQuality { get; set; }
        public uint UserLimit { get; set; } //default 0 = unlimited, max99 (maybe some other max cause server stability via signalr)
        public ServerRegion Region { get; set; } //this defines the hub server you connect to on voice join.
        public ServerSettings? ServerSettings { get; set; } //mapped through inactivechannel
        public ICollection<AccountServerVoiceChannelMute>? Muters { get; set; }
        public ICollection<ServerVoiceInvite>? VoiceInvites { get; set; }
        public ICollection<ServerVoiceChannelPermissionConfiguration>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user
        public ICollection<ServerVoiceChannelRoleConfiguration>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerVoiceChannelRolePermissionConfiguration>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerVoiceChannelMemberSettingsConfiguration>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerVoiceChannelMemberPermissionConfiguration>? MemberPermissions { get; set; } //all memberpermissions linked to this category

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

            builder.HasOne(b => b.Region).WithMany(b => b.VoiceChannels).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.ServerSettings).WithOne(b => b.InactiveChannel).HasForeignKey<ServerSettings>(b => b.InactiveChannelId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasMany(b => b.Muters).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.VoiceInvites).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.AllowedPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.AllowedRoles).WithOne(b => b.Channel).HasForeignKey(b => new { b.ChannelCategoryId, b.RoleId }).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.MemberSettings).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasMany(b => b.MemberPermissions).WithOne(b => b.Channel).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        }
    }
}