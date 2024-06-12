using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category;

public class ServerChannelCategoryRoleConfiguration : IEntityTypeConfiguration<ServerChannelCategoryRole>
{
    public ICollection<ServerChannelCategoryRolePermissionConfiguration>? Permissions { get; set; }

    public void Configure(EntityTypeBuilder<ServerChannelCategoryRole> builder)
    {
        builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId });

        builder.HasOne(b => b.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.ChannelCategory).WithMany().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.ClientCascade);
    }
}
