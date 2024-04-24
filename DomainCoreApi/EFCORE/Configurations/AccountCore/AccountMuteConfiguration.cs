﻿using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountMuteConfiguration : IEntityTypeConfiguration<AccountMute>
    {
        public void Configure(EntityTypeBuilder<AccountMute> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.TimeMuted).ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.ExpirationTime).IsRequired(false);
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Muter).WithMany(b => b.MutedVoices).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}