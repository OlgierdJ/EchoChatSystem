using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryRoleConfiguration : IEntityTypeConfiguration<ServerChannelCategoryRole>
    {
        /*
        //combined pk
        //channelcategory owner
        public ulong ChannelCategoryId { get; set; }
        //base role
        public ulong RoleId { get; set; }

        public ServerChannelCategoryConfiguration ChannelCategory { get; set; }
        public ServerRole Role { get; set; }
        //independent permissions from the global permissions in server.
        //(these permissions are weighed more than the global server permissions except for serveradmin)
        */
        public ICollection<ServerChannelCategoryRolePermissionConfiguration>? Permissions { get; set; }

        public void Configure(EntityTypeBuilder<ServerChannelCategoryRole> builder)
        {
            builder.HasKey(b => new { b.ChannelCategoryId, b.RoleId });

            builder.HasOne(b => b.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.ChannelCategory).WithMany().HasForeignKey(b => b.ChannelCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
