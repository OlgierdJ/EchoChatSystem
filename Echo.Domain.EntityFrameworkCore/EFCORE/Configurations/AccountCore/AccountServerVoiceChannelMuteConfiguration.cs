using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountServerVoiceChannelMuteConfiguration : IEntityTypeConfiguration<AccountServerVoiceChannelMute>
{
    public void Configure(EntityTypeBuilder<AccountServerVoiceChannelMute> builder)
    {
        builder.HasKey(b => new { b.SubjectId, b.MuterId });

        builder.Property(b => b.TimeMuted).HasDefaultValueSql("getdate()");
        builder.Property(b => b.ExpirationTime).IsRequired(false);

        builder.HasOne(b => b.Subject).WithMany(b => b.Muters).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Muter).WithMany().HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.Restrict);
    }
}
