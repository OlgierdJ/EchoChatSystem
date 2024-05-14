using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreLib.DTO.EchoCore.AccountCore;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{

    public class AccountViolationConfiguration : IEntityTypeConfiguration<AccountViolation>
    {
        public void Configure(EntityTypeBuilder<AccountViolation> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Severity).IsRequired();
            builder.Property(b => b.ExpirationDate).IsRequired(false);

            builder.HasOne(b => b.Subject).WithMany(e=>e.Violations).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Issuer).WithMany(e=>e.IssuedViolations).HasForeignKey(b => b.IssuerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Appeal).WithOne(e=>e.Violation).HasForeignKey<AccountViolationAppeal>(e=>e.ViolationId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            
            builder.HasMany(b => b.ConsumedCustomStatusReports).WithOne(b => b.Violation).HasForeignKey(b => b.ViolationId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasMany(b => b.ConsumedMessageReports).WithOne(b => b.Violation).HasForeignKey(b => b.ViolationId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasMany(b => b.ConsumedProfileReports).WithOne(b => b.Violation).HasForeignKey(b => b.ViolationId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        }
    }
}
