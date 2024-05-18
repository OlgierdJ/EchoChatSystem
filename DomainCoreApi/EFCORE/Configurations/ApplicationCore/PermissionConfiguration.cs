using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission> //needs different kinds of application permissions f.eks
                                                                                //("View_Server"), ("Send_Message"), ("Add_Friend"), ("Join_Voice"), ("Delete_Account"), ("Create_Server"), ("Create Chats"),
                                                                                //other generic appwide permissions, etc
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasMany(b => b.Roles).WithMany(e => e.Permissions).UsingEntity<RolePermission>(

                l => l.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId),
                  r => r.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId)
            );
        }
    }
}
