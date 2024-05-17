using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ModerationCore
{
    public class ServerAuditLogConfiguration : IEntityTypeConfiguration<ServerAuditLog>
    {
        public void Configure(EntityTypeBuilder<ServerAuditLog> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TimeLogged).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(b => b.Action).IsRequired();

            builder.HasOne(b => b.Account).WithMany().HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.Server).WithMany(b => b.AuditLogs).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}