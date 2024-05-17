using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountNoteConfiguration : IEntityTypeConfiguration<AccountNote>
    {
        public void Configure(EntityTypeBuilder<AccountNote> builder)
        {
            builder.HasKey(b => new { b.AuthorId, b.SubjectId });
            builder.Property(b => b.Note).HasMaxLength(120).IsRequired();
            builder.HasOne(b => b.Author).WithMany(b => b.NotedAccounts).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
