using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionConfiguration : IEntityTypeConfiguration<ServerPermission>  //reused serverpermission lists all permissions that can be used on the server
    {
        public void Configure(EntityTypeBuilder<ServerPermission> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Description).IsRequired(false);

            builder.HasOne(b => b.Category).WithMany(b => b.Permissions).HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberSettings).WithOne().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.CategoryMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.TextChannelMemberSettings).WithOne().HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);//

            builder.HasMany(b => b.TextChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberSettings).WithOne().HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}