using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class DirectMessageRelationConfiguration : IEntityTypeConfiguration<DirectMessageRelation>
    {
        public void Configure(EntityTypeBuilder<DirectMessageRelation> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.TimeCreated).HasDefaultValueSql("getdate()");
            builder.HasMany(e => e.AccountsInRelation).WithOne(b => b.Relation).HasForeignKey(b => b.RelationId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.Chat).WithOne(e => e.DirectMessageRelation).HasForeignKey<DirectMessageRelation>(b => b.ChatId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
