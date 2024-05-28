using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{

    public class RoleConfiguration : IEntityTypeConfiguration<Role> //needs different kinds of base app roles f.eks
                                                                    //("User"), ("Admin"), ("Moderator"), (System_Owner), ("Tribunal et eller andet"), ("Premium_Sonar"), ("Premium_Sonar_Plus")
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired(); // not mapped most of stuff

            builder.HasMany(e => e.Recipients).WithMany(e => e.Roles).UsingEntity<AccountRole>(
                l => l.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId),
                r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId)
                );
            builder.HasMany(b => b.Permissions).WithMany(e => e.Roles).UsingEntity<RolePermission>(

                  l => l.HasOne(e => e.Permission).WithMany().HasForeignKey(e => e.PermissionId),
                r => r.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId)
            );

            builder.HasData(new Role { Id = 1, Name = "User" });
            builder.HasData(new Role { Id = 2, Name = "Admin" });
            builder.HasData(new Role { Id = 3, Name = "Moderator" });
            builder.HasData(new Role { Id = 4, Name = "System_Owner" });
            builder.HasData(new Role { Id = 5, Name = "Premium_Sonar" });
            builder.HasData(new Role { Id = 6, Name = "Premium_Sonar_Plus" });
        }
    }
}
