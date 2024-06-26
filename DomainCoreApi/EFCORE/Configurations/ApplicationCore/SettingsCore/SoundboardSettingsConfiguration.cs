﻿using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class SoundboardSettingsConfiguration : IEntityTypeConfiguration<SoundboardSettings>
    {
        public void Configure(EntityTypeBuilder<SoundboardSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasOne(b => b.AccountSettings).WithOne(e => e.SoundboardSettings).HasForeignKey<SoundboardSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
