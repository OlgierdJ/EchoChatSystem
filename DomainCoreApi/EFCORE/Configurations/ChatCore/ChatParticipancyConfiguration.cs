using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatParticipancyConfiguration : IEntityTypeConfiguration<ChatParticipancy>
    {
        public void Configure(EntityTypeBuilder<ChatParticipancy> builder)
        {
            builder
                .HasKey(b => new { b.ParticipantId, b.SubjectId });
            builder
               .Property(b => b.TimeJoined).HasDefaultValueSql("getdate()").IsRequired();
            builder.HasOne(b => b.Subject).WithMany(e=>e.Participants).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Participant).WithMany(e=>e.Chats).HasForeignKey(b => b.ParticipantId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(); // have changed ClientCascade to cascade
        }
    }
}
