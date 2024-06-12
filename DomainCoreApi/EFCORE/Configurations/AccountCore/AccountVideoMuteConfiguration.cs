using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore;

public class AccountVideoMuteConfiguration : IEntityTypeConfiguration<AccountVideoMute>
{
    public void Configure(EntityTypeBuilder<AccountVideoMute> builder)
    {
        builder.HasKey(b => new { b.SubjectId, b.MuterId });

        builder.Property(b => b.TimeMuted).HasDefaultValueSql("getdate()");
        builder.Property(b => b.ExpirationTime).IsRequired(false);

        builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Muter).WithMany().HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.Restrict);
    }
}
