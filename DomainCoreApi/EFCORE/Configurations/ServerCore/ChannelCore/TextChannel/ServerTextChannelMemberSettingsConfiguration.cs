using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelMemberSettingsConfiguration : IEntityTypeConfiguration<ServerTextChannelMemberSettings>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelMemberSettings> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.ProfileId });

            builder.HasOne(b => b.Channel).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Profile).WithMany(b => b.TextChannelMemberSettings).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.Permissions).WithOne(b => b.MemberSettings).HasForeignKey(b => new { b.ChannelId, b.ProfileId }).OnDelete(DeleteBehavior.Cascade);
        }
    }
}