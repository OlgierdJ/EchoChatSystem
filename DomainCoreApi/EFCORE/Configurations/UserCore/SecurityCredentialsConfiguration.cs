using CoreLib.Entities.EchoCore.UserCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.UserCore;

public class SecurityCredentialsConfiguration : IEntityTypeConfiguration<SecurityCredentials>
{
    public void Configure(EntityTypeBuilder<SecurityCredentials> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.Salt).IsRequired();
        builder.HasOne(x => x.User).WithOne(x => x.SecurityCredentials).HasForeignKey<SecurityCredentials>(x => x.UserId);
    }
}
