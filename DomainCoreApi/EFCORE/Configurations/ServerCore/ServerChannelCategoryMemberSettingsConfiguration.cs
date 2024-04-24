//using CoreLib.Entities.EchoCore.ServerCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace DomainCoreApi.EFCORE.Configurations.ServerCore
//{
//    public class ServerChannelCategoryMemberSettingsConfiguration : IEntityTypeConfiguration<ServerChannelCategoryMemberSettings>
//    {
//        public void Configure(EntityTypeBuilder<ServerChannelCategoryMemberSettings> builder)
//        {
//            builder.HasKey(ccms => ccms.Id);
//            builder.Property(ccms => ccms.ChannelCategoryId).IsRequired();
//            builder.Property(ccms => ccms.RoleId).IsRequired();

//            // Configure relationships
//            builder.HasOne(ccms => ccms.ChannelCategory)
//                   .WithMany(scc => scc.ChannelCategoryMemberSettings)
//                   .HasForeignKey(ccms => ccms.ChannelCategoryId);

//            builder.HasOne(ccms => ccms.Role)
//                   .WithMany()
//                   .HasForeignKey(ccms => ccms.RoleId);

//            builder.HasMany(ccms => ccms.ChannelCategoryPermissions)
//                   .WithOne(ccp => ccp.ChannelCategoryMemberSettings)
//                   .HasForeignKey(ccp => ccp.ChannelCategoryMemberSettingsId);
//        }
//    }
//}
