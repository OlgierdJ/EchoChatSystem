using CoreLib.Entities.EchoCore.ServerCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore
{
    public class ServerAuditLogConfiguration : IEntityTypeConfiguration<ServerAuditLog>
    {
        public void Configure(EntityTypeBuilder<ServerAuditLog> builder)
        {
            builder.HasKey(sal => sal.Id);
            builder.Property(sal => sal.TimeLogged).IsRequired();
            builder.Property(sal => sal.Action).IsRequired();

            // Configure relationships
            builder.HasOne(sal => sal.Account)
                   .WithMany()
                   .HasForeignKey(sal => sal.AccountId);

            builder.HasOne(sal => sal.Server)
                   .WithMany()
                   .HasForeignKey(sal => sal.ServerId);
        }
    }
}
