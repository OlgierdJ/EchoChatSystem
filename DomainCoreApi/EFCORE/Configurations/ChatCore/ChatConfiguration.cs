﻿using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.Name)
                .IsRequired();
            builder
               .Property(b => b.IconUrl)
               .IsRequired();
            builder
                .Property(b => b.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();

            builder.HasMany(b => b.MessageTrackers).WithOne(b => b.CoOwner).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Messages).WithOne(b => b.MessageHolder).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Mutes).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Invites).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(b => b.Pinboard).WithOne(b => b.Owner).HasForeignKey<ChatPinboard>(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Participants).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            //builder.HasMany(b => b.Participants).WithMany(b => b.Chats).UsingEntity<ChatParticipancy>(
            //        l => l.HasOne(e => e.Participant).WithMany().HasForeignKey(e => e.ParticipantId),
            //        r => r.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId)
            //    ); //maybe take config from chatparticipant config file
        }
    }
}
