using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberSettingsConfiguration : IEntityTypeConfiguration<ServerVoiceChannelMemberSettings>
    {
        public ICollection<ServerVoiceChannelMemberPermissionConfiguration>? Permissions { get; set; }

        public void Configure(EntityTypeBuilder<ServerVoiceChannelMemberSettings> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.ProfileId });

            builder.HasMany(b => b.Permissions).WithOne(b => b.MemberSettings).HasForeignKey(b => new { b.ChannelId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Channel).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Profile).WithMany(b => b.VoiceChannelMemberSettings).HasForeignKey(b => b.ProfileId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}