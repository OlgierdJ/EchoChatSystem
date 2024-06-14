using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountSessionConfiguration : IEntityTypeConfiguration<AccountSession>
{
    public void Configure(EntityTypeBuilder<AccountSession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(b => b.AccessToken).HasMaxLength(512).IsRequired();
        builder.Property(b => b.RefreshToken).HasMaxLength(512).IsRequired();
        builder.Property(b => b.Location).HasMaxLength(100).IsRequired();
        builder.Property(b => b.DeviceId).HasMaxLength(255).IsRequired();
        builder.Property(b => b.TimeStarted).ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.ExpirationTime).IsRequired(false);
        builder.Property(b => b.TimeStopped).IsRequired(false);
        builder.HasOne(b => b.Account).WithMany(b => b.Sessions).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
//