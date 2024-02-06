using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;

namespace DomainCoreApi.EFCORE.Configurations
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
                .Property(b => b.TimeCreated).ValueGeneratedOnAdd()
                .IsRequired();
            builder.HasMany(b=>b.Messages).WithOne(b=>b.MessageHolder).HasForeignKey(b=>b.MessageHolderId).IsRequired();
            builder.HasMany(b=>b.Mutes).WithOne(b=>b.Subject).HasForeignKey(b=>b.SubjectId).IsRequired();
            builder.HasMany(b=>b.Invites).WithOne(b=>b.Subject).HasForeignKey(b=>b.SubjectId).IsRequired();
            builder.HasMany(b=>b.Participants).WithOne(b=>b.Subject).HasForeignKey(b=>b.SubjectId).IsRequired();
            builder.HasOne(b=>b.Pinboard).WithOne(b=>b.Owner).HasForeignKey<ChatPinboard>(b=>b.OwnerId).IsRequired();
        }
    }
}
