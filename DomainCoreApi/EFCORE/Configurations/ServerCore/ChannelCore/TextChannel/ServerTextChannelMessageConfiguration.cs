using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessageConfiguration : IEntityTypeConfiguration<ServerTextChannelMessage>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelMessage> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b=>b.Content).IsRequired();

            builder.Property(b => b.TimeSent).HasDefaultValueSql("getdate()").IsRequired();

            builder.HasOne(b => b.Author).WithMany(e => e.ChannelMessages).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(b => b.MessageHolder).WithMany(e => e.Messages).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Parent).WithMany(e => e.Children).HasForeignKey(b => b.ParentId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

            builder.HasMany(b => b.Attachments).WithOne(e => e.Message).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.MessageTrackers).WithOne(e => e.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

            builder.HasOne(b => b.MessagePin).WithOne(b => b.Message).HasForeignKey<ServerTextChannelMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();

        }
    }
}
