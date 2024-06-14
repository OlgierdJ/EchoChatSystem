using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountServerFolderConfiguration : IEntityTypeConfiguration<AccountServerFolder>
{
    public void Configure(EntityTypeBuilder<AccountServerFolder> builder)
    {
        builder.HasKey(b => new { b.Id, b.Name }); //figure out this shitty id combine ownerid, some other autoincrement id.
        builder.Property(b => b.Name).HasMaxLength(256).IsRequired(); //does not have to be unique.
        builder.Property(b => b.Importance).IsRequired(); //ordering
        builder.Property(b => b.Colour).HasMaxLength(32).IsRequired(false);

        builder.HasOne(b => b.Account).WithMany(b => b.Folders).HasForeignKey(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired(); //owner
        builder.HasMany(b => b.Servers).WithOne(b => b.Folder).HasForeignKey(b => new { b.FolderId, b.FolderName }).OnDelete(DeleteBehavior.Restrict).IsRequired(false); //content
    }
}
