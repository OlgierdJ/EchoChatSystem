using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language> //needs supported languages just start with ("dansk"), ("english"), ("deutsch")
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasMany(b => b.AccountSettings).WithOne(e => e.Language).HasForeignKey(b => b.LanguageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
