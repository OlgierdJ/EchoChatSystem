//using CoreLib.Entities.EchoCore.ServerCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DomainCoreApi.EFCORE.Configurations.ServerCore
//{
//    public class ServerConfiguration : IEntityTypeConfiguration<Server>
//    {
//        public void Configure(EntityTypeBuilder<Server> builder)
//        {
//            builder.HasKey(s => s.Id);
//            builder.Property(s => s.Name).HasMaxLength(255).IsRequired();
//            builder.Property(s => s.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();

//            // Configure relationships
//            //builder.HasMany(s => s.Settings)
//            //       .WithOne(ss => ss.Server)
//            //       .HasForeignKey(ss => ss.ServerId);

//            //builder.HasMany(s => s.Invites)
//            //       .WithOne(si => si.Server)
//            //       .HasForeignKey(si => si.ServerId);

//            //builder.HasMany(s => s.Events)
//            //       .WithOne(se => se.Server)
//            //       .HasForeignKey(se => se.ServerId);

//            //builder.HasMany(s => s.ChannelCategories)
//            //       .WithOne(scc => scc.Server)
//            //       .HasForeignKey(scc => scc.ServerId);

//            //builder.HasMany(s => s.TextChannels)
//            //       .WithOne(stc => stc.Server)
//            //       .HasForeignKey(stc => stc.ServerId);

//            //builder.HasMany(s => s.VoiceChannels)
//            //       .WithOne(svc => svc.Server)
//            //       .HasForeignKey(svc => svc.ServerId);

//            //builder.HasMany(s => s.Members)
//            //       .WithOne(sm => sm.Server)
//            //       .HasForeignKey(sm => sm.ServerId);
//        }
//    }
//}
