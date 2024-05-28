using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountAccountVolumeConfiguration : IEntityTypeConfiguration<AccountAccountVolume>
    {
        public void Configure(EntityTypeBuilder<AccountAccountVolume> builder)
        {
            builder.HasKey(b => new { b.OwnerId, b.SubjectId });
            builder.HasOne(b => b.Owner).WithMany(b => b.PersonalAccountVolumes).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            
        }
    }
}
