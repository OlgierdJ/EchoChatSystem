//using CoreLib.Entities.EchoCore.ServerCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace DomainCoreApi.EFCORE.Configurations.ServerCore
//{
//    public class ServerChannelCategoryConfiguration : IEntityTypeConfiguration<ServerChannelCategory>
//    {
//        public void Configure(EntityTypeBuilder<ServerChannelCategory> builder)
//        {
//            builder.HasKey(scc => scc.Id);
//            builder.Property(scc => scc.Name).IsRequired();
//            builder.Property(scc => scc.IsPrivate).IsRequired();

//            // Configure relationships
//            builder.HasOne(scc => scc.Server)
//                   .WithMany(s => s.ChannelCategories)
//                   .HasForeignKey(scc => scc.ServerId);

//            builder.HasMany(scc => scc.TextChannels)
//                   .WithOne(stc => stc.ChannelCategory)
//                   .HasForeignKey(stc => stc.ChannelCategoryId);

//            builder.HasMany(scc => scc.VoiceChannels)
//                   .WithOne(svc => svc.ChannelCategory)
//                   .HasForeignKey(svc => svc.ChannelCategoryId);

//            builder.HasMany(scc => scc.ChannelCategoryMemberSettings)
//                   .WithOne(ccms => ccms.ChannelCategory)
//                   .HasForeignKey(ccms => ccms.ChannelCategoryId);
//        }
//    }
//}
