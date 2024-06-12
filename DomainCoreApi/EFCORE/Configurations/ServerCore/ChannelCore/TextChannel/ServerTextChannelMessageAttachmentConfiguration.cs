using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMessageAttachmentConfiguration : IEntityTypeConfiguration<ServerTextChannelMessageAttachment>
{
    public void Configure(EntityTypeBuilder<ServerTextChannelMessageAttachment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.FileLocationURL).IsRequired();

        builder.HasOne(b => b.Message).WithMany(b => b.Attachments).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    }
}