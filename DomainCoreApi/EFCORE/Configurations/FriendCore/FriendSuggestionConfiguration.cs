﻿using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreLib.Entities.EchoCore.FriendCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class FriendSuggestionConfiguration : IEntityTypeConfiguration<FriendSuggestion>
    {
        public void Configure(EntityTypeBuilder<FriendSuggestion> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimeSuggested)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder
               .Property(b => b.Declined)
               .IsRequired();
            builder.HasOne(b => b.Suggestion)
                .WithMany()
                .HasForeignKey(b => b.SuggestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(b => b.Receiver)
                .WithMany(e => e.FriendSuggestions)
                .HasForeignKey(b => b.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasIndex(b=>new { b.ReceiverId, b.SuggestionId }).IsUnique();
        }
    }
}
