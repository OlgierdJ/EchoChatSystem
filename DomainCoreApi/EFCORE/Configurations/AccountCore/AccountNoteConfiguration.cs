using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountNoteConfiguration : IEntityTypeConfiguration<AccountNote>
    {
        public void Configure(EntityTypeBuilder<AccountNote> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Note).HasMaxLength(120).IsRequired();
            builder.HasOne(b => b.Author).WithMany(b => b.NotedAccounts).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
