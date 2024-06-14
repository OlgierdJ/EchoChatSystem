using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore;

public class BillingInformationConfiguration : IEntityTypeConfiguration<BillingInformation>
{
    public void Configure(EntityTypeBuilder<BillingInformation> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.BillingInformation).HasForeignKey<BillingInformation>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
