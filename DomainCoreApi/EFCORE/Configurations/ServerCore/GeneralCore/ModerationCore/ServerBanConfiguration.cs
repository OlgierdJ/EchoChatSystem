using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ModerationCore
{
    public class ServerBanConfiguration : IEntityTypeConfiguration<ServerBan> //needs id cause ban can be specific
    {
        public void Configure(EntityTypeBuilder<ServerBan> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Reason).IsRequired(false);
            builder.Property(b => b.ExpirationTime).HasDefaultValue(null).IsRequired(false);//tænker at hvis der ikke bliver en sendt en værdi så er den et perma
            builder.Property(b => b.TimeKeepMessagesBefore).IsRequired();

            builder.HasOne(b => b.Account).WithMany().HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.Server).WithMany(b => b.BanList).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}