using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountActivityStatusConfiguration : IEntityTypeConfiguration<AccountActivityStatus>
{
    public void Configure(EntityTypeBuilder<AccountActivityStatus> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).HasMaxLength(128).IsRequired();
        builder.Property(b => b.Description).HasMaxLength(200).IsRequired(false);
        builder.Property(b => b.Icon).HasMaxLength(200).IsRequired();
        builder.Property(b => b.IconColor).HasMaxLength(16).IsRequired();
        builder.HasMany(b => b.Accounts).WithOne(b => b.ActivityStatus).HasForeignKey(b => b.ActivityStatusId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasData(new AccountActivityStatus { Id = 1, Name = "Online", Description = "", IconColor = "Success", Icon = "Icons.Material.Filled.Circle" });
        builder.HasData(new AccountActivityStatus { Id = 2, Name = "Away", Description = "", IconColor = "Warning", Icon = "Icons.Material.Filled.Brightness2" });
        builder.HasData(new AccountActivityStatus { Id = 3, Name = "Busy", Description = "You will not receive any desktop notifications.", IconColor = "Error", Icon = "Icons.Material.Filled.RemoveCircle" });
        builder.HasData(new AccountActivityStatus { Id = 4, Name = "Offline", Description = "", IconColor = "Dark", Icon = "Icons.Material.Filled.StopCircle" });
        builder.HasData(new AccountActivityStatus { Id = 5, Name = "Invisible", Description = "You will not appear online, but have full access to all of Echo.", IconColor = "Dark", Icon = "Icons.Material.Filled.StopCircle" });
    }


}