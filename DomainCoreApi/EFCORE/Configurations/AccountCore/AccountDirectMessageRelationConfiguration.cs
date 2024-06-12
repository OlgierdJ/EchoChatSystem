using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore;

public class AccountDirectMessageRelationConfiguration : IEntityTypeConfiguration<AccountDirectMessageRelation>
{
    public void Configure(EntityTypeBuilder<AccountDirectMessageRelation> builder)
    {
        builder.HasKey(b => new { b.OwnerId, b.RelationId });
        builder.HasOne(b => b.Owner).WithMany(e => e.DirectMessageRelations).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasOne(b => b.Relation).WithMany(e => e.AccountsInRelation).HasForeignKey(b => b.RelationId).OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
