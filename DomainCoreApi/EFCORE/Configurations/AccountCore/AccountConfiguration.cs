using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(256).IsRequired();
            builder.Property(b=> b.TimeCreated).ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.TimeLastLogon);
            builder.Property(b => b.FocusModeEnabled).HasDefaultValue(true).IsRequired();
            builder.HasOne(b => b.ActivityStatus).WithMany(b=>b.Accounts).HasForeignKey(b=>b.ActivityStatusId).OnDelete(DeleteBehavior.Cascade); //skal lige kig på det kan ikke lige find ud hvorfor den ikke vil får en "Compiler Error CS0452"

        }
    }
}
