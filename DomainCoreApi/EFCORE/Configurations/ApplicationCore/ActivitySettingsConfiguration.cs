using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreLib.Entities.EchoCore.ApplicationCore;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class ActivitySettingsConfiguration : IEntityTypeConfiguration<ActivitySettings>
    {
        public void Configure(EntityTypeBuilder<ActivitySettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasOne(b=>b.AccountSettings).WithOne(e => e.ActivitySettings).HasForeignKey<ActivitySettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
