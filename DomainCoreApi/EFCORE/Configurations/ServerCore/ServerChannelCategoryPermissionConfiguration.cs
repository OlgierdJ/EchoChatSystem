//using CoreLib.Entities.EchoCore.ServerCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DomainCoreApi.EFCORE.Configurations.ServerCore
//{
//    public class ServerChannelCategoryPermissionConfiguration : IEntityTypeConfiguration<ServerChannelCategoryPermission>
//    {
//        public void Configure(EntityTypeBuilder<ServerChannelCategoryPermission> builder)
//        {
//            builder.HasKey(ccp => ccp.Id);
//            builder.HasOne(ccp => ccp.ChannelCategoryMemberSettings)
//                  .WithMany(ccms => ccms.ChannelCategoryPermissions)
//                  .HasForeignKey(ccp => ccp.ChannelCategoryMemberSettingsId);
//        }
//    }
//}