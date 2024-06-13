using Echo.Domain.Shared.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore;

public class ChatMuteConfiguration : IEntityTypeConfiguration<ChatMute>
{
    public void Configure(EntityTypeBuilder<ChatMute> builder)
    {
        builder.HasKey(b => new { b.MuterId, b.SubjectId });
        builder.Property(b => b.TimeMuted).HasDefaultValueSql("getdate()").IsRequired();
        builder.Property(b => b.ExpirationTime).IsRequired(false);
        builder.HasOne(b => b.Subject).WithMany(e => e.Muters).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(b => b.Muter).WithMany(b => b.MutedChats).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
