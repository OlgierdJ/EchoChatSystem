using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceInviteConfiguration : IEntityTypeConfiguration<ServerVoiceInvite>
{

    public void Configure(EntityTypeBuilder<ServerVoiceInvite> builder)
    {
        builder.HasKey(b => new { b.SubjectId, b.InviterId });

        builder.Property(b => b.InviteCode).IsRequired();
        builder.Property(b => b.ExpirationTime).IsRequired(false);
        builder.Property(b => b.TimesUsed).IsRequired();
        builder.Property(b => b.TotalUses).IsRequired();
        builder.Property(b => b.GuestInvite).IsRequired();

        //builder.HasOne(b => b.Subject).WithMany(e=>e.VoiceInvites).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Inviter).WithMany().HasForeignKey(b => b.InviterId).OnDelete(DeleteBehavior.Restrict);
        //builder.HasOne(b => b.Channel).WithMany(b => b.Invites).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);
    }
}
/*
    public string InviteCode { get; set; } //essentially unique id can never be reused
    public DateTime? ExpirationTime { get; set; } // null = permanent
    public int TimesUsed { get; set; }
    public int TotalUses { get; set; } //0 means unlimited //mayb review / set to nullable?
*/