using CoreLib.Entities.EchoCore.ServerCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore
{
    public class ServerBanConfiguration : IEntityTypeConfiguration<ServerBan>
    {
        public void Configure(EntityTypeBuilder<ServerBan> builder)
        {
            builder.HasKey(sb => sb.Id);
            builder.Property(sb => sb.Reason).IsRequired(false);
            builder.Property(sb => sb.TimeExpired).IsRequired(false);

            // Configure relationships
            builder.HasOne(sb => sb.Account)
                   .WithMany()
                   .HasForeignKey(sb => sb.AccountId);
        }
    }
}
