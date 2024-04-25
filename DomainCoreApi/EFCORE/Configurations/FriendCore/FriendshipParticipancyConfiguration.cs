using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    //public class FriendshipParticipancyConfiguration : IEntityTypeConfiguration<FriendshipParticipancy>
    //{
    //    public void Configure(EntityTypeBuilder<FriendshipParticipancy> builder)
    //    {
    //        //builder
    //        //    .HasKey(b => new { b.AccountId, b.FriendshipId });
    //        //builder.HasOne(b => b.Account)
    //        //    .WithMany()
    //        //    .HasForeignKey(b => b.AccountId)
    //        //    .OnDelete(DeleteBehavior.Cascade)
    //        //    .IsRequired();
    //        //builder.HasOne(b => b.Friendship)
    //        //    .WithMany()
    //        //    .HasForeignKey(b => b.FriendshipId)
    //        //    .OnDelete(DeleteBehavior.Cascade)
    //        //    .IsRequired();
    //        //builder.HasIndex(b => new { b.AccountId, b.FriendshipId }).IsUnique();
    //    }
    //}
}
