using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore;

public class AccountSoundboardMuteConfiguration : IEntityTypeConfiguration<AccountSoundboardMute>
{
    public void Configure(EntityTypeBuilder<AccountSoundboardMute> builder)
    {
        builder.HasKey(b => new { b.MuterId, b.SubjectId });
        builder.Property(b => b.TimeMuted).ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.ExpirationTime).IsRequired(false);
        builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        builder.HasOne(b => b.Muter).WithMany(b => b.MutedSoundboards).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
