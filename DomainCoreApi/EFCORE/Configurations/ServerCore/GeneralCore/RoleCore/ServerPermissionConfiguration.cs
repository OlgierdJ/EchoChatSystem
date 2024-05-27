using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionConfiguration : IEntityTypeConfiguration<ServerPermission>
    //needs different types of permissions stolen straight from discord channel or server permissions such as:
    /*
     * ("Administrator", "Members with this permission will have every permission and will also bypass channel specific permissions or restrictions (for example, these members would get access to all private channels) **This is a dangerous permission to grant.")
     * ("Manage Events", "Allows members to edit and cancel events.")
     * ("Create Events", "Allows members to create events.")
     * ("Set Voice Channel Status", "Allows members to create and edit voice channel status.")
     * ("Move Members", "Allows members to disconnect or move other members between voice channels that the member with this permission has access to.")
     * ("Deafen Members", "Allows members to deafen other members, which means they wont be able to speak or hear others.") //server deafen
     * ("Mute Members", "Allows members to mute other members, which means they wont be able to speak.") //server mute
     * ("Priority Speaker", "") //lowers others volume
     * ("")
     * etc
     */
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

            builder.HasMany(b => b.CategoryRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.ServerVoiceChannelRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.TextChannelRolePermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.TextChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberSettings).WithOne().HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(b => b.VoiceChannelMemberPermissions).WithOne(b => b.Permission).HasForeignKey(b => b.PermissionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}