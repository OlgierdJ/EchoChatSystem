using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore
{
    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();

            builder.HasOne(b=>b.Settings).WithOne(b=>b.Server).HasForeignKey<ServerSettings>(b=>b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            builder.HasMany(b => b.Events).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Invites).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Muters).WithOne(b => b.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            builder.HasMany(b => b.AuditLogs).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.BanList).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Emotes).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.SoundboardSounds).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            builder.HasMany(b => b.ChannelCategories).WithOne(b => b.Server).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.TextChannels).WithOne(b => b.Owner).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.VoiceChannels).WithOne(b => b.Owner).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
