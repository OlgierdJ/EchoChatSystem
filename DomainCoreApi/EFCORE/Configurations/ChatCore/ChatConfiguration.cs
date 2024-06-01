using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
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
                .IsRequired(); // maybe make non required=?
            builder
               .Property(b => b.IconUrl)
               .IsRequired(false);
            builder
                .Property(b => b.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();

            builder.HasMany(b => b.MessageTrackers).WithOne(b => b.CoOwner).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Messages).WithOne(b => b.MessageHolder).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.Cascade).IsRequired(false); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Mutes).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.Invites).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            //builder.HasOne(b => b.Pinboard).WithOne(b => b.Owner).HasForeignKey<ChatPinboard>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.DirectMessageRelation).WithOne(b => b.Chat).HasForeignKey<DirectMessageRelation>(b => b.ChatId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasMany(b => b.Participants).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.PinnedMessages).WithOne(b => b.Pinboard).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            //builder.HasMany(b => b.Participants).WithMany(b => b.Chats).UsingEntity<ChatParticipancy>(
            //        l => l.HasOne(e => e.Participant).WithMany().HasForeignKey(e => e.ParticipantId),
            //        r => r.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId)
            //    ); //maybe take config from chatparticipant config file
        }
    }
}
