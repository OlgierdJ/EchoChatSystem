using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

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
                .Property(b => b.TimeCreated).ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasMany(b => b.MessageTrackers).WithOne(b => b.Holder).HasForeignKey(b => b.HolderId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Messages).WithOne(b => b.MessageHolder).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //cascade delete messages if the chat is deleted
            builder.HasMany(b => b.Mutes).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.Invites).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.Pinboard).WithOne(b => b.Owner).HasForeignKey<ChatPinboard>(b => b.OwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.Participants).WithMany(b => b.Chats).UsingEntity<ChatParticipancy>(); //maybe take config from chatparticipant config file
        }
    }
}
