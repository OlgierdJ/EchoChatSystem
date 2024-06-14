using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore;

public class ServerProfileServerRoleConfiguration : IEntityTypeConfiguration<ServerProfileServerRole>
{
    public void Configure(EntityTypeBuilder<ServerProfileServerRole> builder)
    {
        builder.HasKey(b => new { b.AccountId, b.RoleId });

        builder.Property(b => b.TimeGranted).IsRequired();

        builder.HasOne(b => b.Profile).WithMany(b => b.Roles).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
    }
}
