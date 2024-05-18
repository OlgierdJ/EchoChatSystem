using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelMemberSettingsConfiguration : IEntityTypeConfiguration<ServerTextChannelMemberSettings>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelMemberSettings> builder)
        {
            builder.HasKey(b => new { b.ChannelId, b.AccountId });

            builder.HasOne(b => b.Channel).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Profile).WithMany(b => b.TextChannelMemberSettings).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasMany(b => b.Permissions).WithOne(b => b.MemberSettings).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}