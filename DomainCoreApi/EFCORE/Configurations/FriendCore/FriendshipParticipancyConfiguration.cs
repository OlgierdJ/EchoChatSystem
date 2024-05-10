using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class FriendshipParticipancyConfiguration : IEntityTypeConfiguration<FriendshipParticipancy>
    {
        public void Configure(EntityTypeBuilder<FriendshipParticipancy> builder)
        {
            builder
                .HasKey(b => new { b.ParticipantId, b.SubjectId });
            builder.HasOne(b => b.Participant)
                .WithMany()
                .HasForeignKey(b => b.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(b => b.Subject)
                .WithMany()
                .HasForeignKey(b => b.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            ////builder.HasIndex(b => new { b.ParticipantId, b.SubjectId }).IsUnique();
            builder.Property(e=>e.TimeJoined).HasDefaultValueSql("getdate()").IsRequired();
        }
    }
}
