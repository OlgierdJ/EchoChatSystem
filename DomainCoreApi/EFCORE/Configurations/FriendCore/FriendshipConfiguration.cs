using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimeCreated)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>();
        }
    }
}
