using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country> //add default values for different kinds of countries f.eks danmark, svarige, deutschland, england, france, zhonghuan, etc USA US A U S A
    {
        public string Name { get; set; }
        public ICollection<PaymentMethod>? PaymentMethods { get; set; }

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name);

            builder.HasMany(b => b.PaymentMethods).WithOne(b=>b.Country).HasForeignKey(b => b.CountryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new Country { Id = 1, Name = "Danmark" });
            builder.HasData(new Country { Id = 2, Name = "Sverige" });
            builder.HasData(new Country { Id = 3, Name = "Noreg" });
            builder.HasData(new Country { Id = 4, Name = "Deutschland" });
            builder.HasData(new Country { Id = 5, Name = "United Kingdom" });
            builder.HasData(new Country { Id = 6, Name = "La France" });
            builder.HasData(new Country { Id = 7, Name = "华人(Chinese)" });
            builder.HasData(new Country { Id = 8, Name = "日本(Japan)" });
            builder.HasData(new Country { Id = 9, Name = "남한(south korea)" });
            builder.HasData(new Country { Id = 10, Name = "United States of America" });
        }
    }
}