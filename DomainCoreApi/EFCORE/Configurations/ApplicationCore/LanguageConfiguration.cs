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

            builder.HasData(new Language { Id = 1, Name = "Dansk", LanguageCode="da-DK" });
            builder.HasData(new Language { Id = 2, Name = "Svenska" ,LanguageCode="se-SV"});
            builder.HasData(new Language { Id = 3, Name = "Norsk", LanguageCode="no-NO"});
            builder.HasData(new Language { Id = 4, Name = "Deutsch",LanguageCode = "de-DE" });
            builder.HasData(new Language { Id = 5, Name = "English (UK)", LanguageCode= "en-GB" });
            builder.HasData(new Language { Id = 6, Name = "Français", LanguageCode="fr-FR" });
            builder.HasData(new Language { Id = 7, Name = "中文 (traditional Chinese)", LanguageCode= "zh-CN" });
            builder.HasData(new Language { Id = 8, Name = "日本語 (Japanese)", LanguageCode="ja-JP" });
            builder.HasData(new Language { Id = 9, Name = "한국어 (korean)", LanguageCode = "ko-KR" });
            builder.HasData(new Language { Id = 10, Name = "English (USA)", LanguageCode = "en-Us" });
        }
    }
}
