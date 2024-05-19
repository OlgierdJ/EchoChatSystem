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

            builder.Property(b => b.Name).IsRequired(); // not mapped most of stuff
            builder.Property(b => b.LanguageCode).IsRequired(); // not mapped most of stuff

            builder.HasMany(b => b.AccountSettings).WithOne(e => e.Language).HasForeignKey(b => b.LanguageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            builder.HasData(new Language { Id = 1, Name = "Danmark",LanguageCode="DK" });
            builder.HasData(new Language { Id = 2, Name = "Sverige" ,LanguageCode="Sv"});
            builder.HasData(new Language { Id = 3, Name = "Noreg", LanguageCode="No"});
            builder.HasData(new Language { Id = 4, Name = "Deutschland",LanguageCode = "De" });
            builder.HasData(new Language { Id = 5, Name = "United Kingdom", LanguageCode= "en-gb" });
            builder.HasData(new Language { Id = 6, Name = "La France", LanguageCode="Fr" });
            builder.HasData(new Language { Id = 7, Name = "中国(China)", LanguageCode= "zh-CN" });
            builder.HasData(new Language { Id = 8, Name = "日本(Japan)",LanguageCode="Jp" });
            builder.HasData(new Language { Id = 9, Name = "남한(south korea)",LanguageCode = "ko" });
            builder.HasData(new Language { Id = 10, Name = "United States of America",LanguageCode = "en-us" });
        }
    }
}
