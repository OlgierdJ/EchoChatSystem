using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.UserCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.UserCore;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PhoneNumber).HasMaxLength(16).IsRequired(false);
        builder.Property(x => x.EmailConfirmed).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.PhoneNumberConfirmed).IsRequired();
        builder.Property(x => x.PasswordSetDate).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.HasOne(x => x.Account).WithOne(b => b.User).HasForeignKey<Account>(x => x.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasOne(x => x.SecurityCredentials).WithOne(x => x.User).HasForeignKey<SecurityCredentials>(x => x.UserId);
    }
}
