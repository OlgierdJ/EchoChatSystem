//using CoreLib.Entities.EchoCore.ServerCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace DomainCoreApi.EFCORE.Configurations.ServerCore
//{
//    public class ServerChannelPermissionConfiguration : IEntityTypeConfiguration<ServerChannelPermission>
//    {
//        public void Configure(EntityTypeBuilder<ServerChannelPermission> builder)
//        {
//            builder.HasKey(scp => scp.Id);
//            builder.Property(scp => scp.Name).HasMaxLength(100).IsRequired();
//            builder.Property(scp => scp.PermissionCategory).HasMaxLength(100).IsRequired();
//            builder.Property(scp => scp.Description).HasMaxLength(150).IsRequired();
//        }
//    }
//}
