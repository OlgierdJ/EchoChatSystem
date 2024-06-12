using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel;

public class AccountServerTextChannelMuteConfiguration : IEntityTypeConfiguration<AccountServerTextChannelMute>
{
    public void Configure(EntityTypeBuilder<AccountServerTextChannelMute> builder)
    {
        builder.HasKey(b => new { b.MuterId, b.SubjectId });

        builder.Property(b => b.TimeMuted).HasDefaultValueSql("getdate()");
        builder.Property(b => b.ExpirationTime).IsRequired(false);

        builder.HasOne(b => b.Subject).WithMany(b => b.Muters).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(b => b.Muter).WithMany(e => e.MutedTextChannels).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.Restrict);
    }
}