using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.Profile;

public class ReportedProfileConfiguration : IEntityTypeConfiguration<ReportedProfile>
{
    public void Configure(EntityTypeBuilder<ReportedProfile> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.DisplayName);
        builder
            .Property(b => b.AvatarFileURL);
        builder
            .Property(b => b.BannerColor);
        builder
           .Property(b => b.About).IsRequired(false);
        builder.HasOne(e => e.Account).WithMany(e => e.ReportedProfiles).HasForeignKey(e => e.AccountId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(e => e.Report).WithOne(e => e.Subject).HasForeignKey<ProfileReport>(e => e.SubjectId).OnDelete(DeleteBehavior.ClientCascade);

        //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
        //{
        //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
        //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
        //});
    }
}
