using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelMemberSettingsConfiguration : IEntityTypeConfiguration<ServerVoiceChannelMemberSettings>
{

    public void Configure(EntityTypeBuilder<ServerVoiceChannelMemberSettings> builder)
    {
        builder.HasKey(b => new { b.ChannelId, b.AccountId });

        builder.HasMany(b => b.Permissions).WithOne(b => b.MemberSettings).HasForeignKey(b => new { b.ChannelId, b.AccountId }).OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(b => b.Channel).WithMany(b => b.MemberSettings).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Profile).WithMany(b => b.VoiceChannelMemberSettings).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Server).WithMany(b => b.VoiceChannelMemberSettings).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.NoAction);
    }
}