using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryConfiguration : IEntityTypeConfiguration<ServerChannelCategory>
    {
        public void Configure(EntityTypeBuilder<ServerChannelCategory> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Importance).IsRequired();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.IsPrivate).IsRequired();

            builder.HasOne(b => b.Server).WithMany(b => b.ChannelCategories).HasForeignKey(b => b.ServerId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            // ved ik helt om det skal være Cascade 
            builder.HasMany(b => b.AllowedPermissions).WithOne(b => b.ChannelCategory).HasForeignKey(b => b.ChannelCategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.RolePermissions).WithOne(b => b.ChannelCategory).HasForeignKey(b => b.ChannelCategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.MemberSettings).WithOne(b => b.ChannelCategory).HasForeignKey(b => b.ChannelCategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.MemberPermissions).WithOne(b => b.ChannelCategory).HasForeignKey(b => b.ChannelCategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.TextChannels).WithOne().HasForeignKey(b => b.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(b => b.VoiceChannels).WithOne().HasForeignKey(b => b.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
