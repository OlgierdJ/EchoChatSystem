using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class StreamerModeSettingsConfiguration : IEntityTypeConfiguration<StreamerModeSettings>
    {
        public void Configure(EntityTypeBuilder<StreamerModeSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasOne(b => b.AccountSettings).WithOne(e => e.StreamerModeSettings).HasForeignKey<StreamerModeSettings>(b => b.AccountSettingsId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
