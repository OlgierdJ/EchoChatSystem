using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore;

public class ServerRoleConfiguration : IEntityTypeConfiguration<ServerRole>
{
    public void Configure(EntityTypeBuilder<ServerRole> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Importance).IsRequired();
        builder.Property(b => b.Colour).IsRequired();
        builder.Property(b => b.IconURL).IsRequired();
        builder.Property(b => b.DisplaySeperatelyFromOnlineMembers).IsRequired();
        builder.Property(b => b.AllowAnyoneToMention).IsRequired();
        builder.Property(b => b.IsAdmin).IsRequired();

        builder.HasMany(b => b.ChannelCategoryRoles).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.TextChannelRoles).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.VoiceChannelRoles).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.ChannelCategoryRolePermissions).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.TextChannelRolePermissions).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.VoiceChannelRolePermissions).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Permissions).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.Recipients).WithOne(b => b.Role).HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
    }
}
