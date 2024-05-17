using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerProfileServerRoleConfiguration : IEntityTypeConfiguration<ServerProfileServerRole>
    {
        public ulong ProfileId { get; set; }
        public ulong RoleId { get; set; }
        public DateTime TimeGranted { get; set; }
        public ServerProfile Profile { get; set; }
        public ServerRole Role { get; set; }

        public void Configure(EntityTypeBuilder<ServerProfileServerRole> builder)
        {
            builder.HasKey(b => new { b.ProfileId, b.RoleId });

            builder.Property(b => b.TimeGranted).IsRequired();

            builder.HasOne(b => b.Profile).WithMany(b => b.Roles).HasForeignKey(b => b.ProfileId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Role).WithMany().HasForeignKey(b => b.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
