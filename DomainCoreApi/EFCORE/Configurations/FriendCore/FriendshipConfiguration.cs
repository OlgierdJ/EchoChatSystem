using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();
            builder.HasMany(e=>e.Participants).WithOne(e=>e.Subject).HasForeignKey(e=>e.SubjectId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(
            //    l => l.HasOne(e => e.Participant).WithMany().HasForeignKey(e => e.ParticipantId),
            //    r => r.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId)
            //);
        }
    }
}
