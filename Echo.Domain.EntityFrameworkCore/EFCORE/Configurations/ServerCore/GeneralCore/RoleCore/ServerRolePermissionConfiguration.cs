using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore;

public class ServerRolePermissionConfiguration : IEntityTypeConfiguration<ServerRolePermission>
{
    public void Configure(EntityTypeBuilder<ServerRolePermission> builder)
    {
        builder.HasKey(b => new { b.RoleId, b.PermissionId });

        builder.Property(b => b.State).HasDefaultValue(null).IsRequired(false);

        builder.HasOne(b => b.Role).WithMany(b => b.Permissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Permission).WithMany(b => b.RolePermissions).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
