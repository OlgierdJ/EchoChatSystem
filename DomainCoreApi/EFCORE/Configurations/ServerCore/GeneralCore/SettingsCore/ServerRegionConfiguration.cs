using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerRegionConfiguration : IEntityTypeConfiguration<ServerRegion>
    {
        public void Configure(EntityTypeBuilder<ServerRegion> builder)
        {
            builder.HasKey(b=>b.Id);

            builder.Property(b=>b.Name).IsRequired();
            builder.Property(b=>b.Icon).IsRequired();
            builder.Property(b=>b.RegionServerURL).IsRequired();

            builder.HasMany(b=>b.VoiceChannels).WithOne(b=>b.Region).HasForeignKey(b=>b.RegionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b=>b.ServerSettings).WithOne(b=>b.Region).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}