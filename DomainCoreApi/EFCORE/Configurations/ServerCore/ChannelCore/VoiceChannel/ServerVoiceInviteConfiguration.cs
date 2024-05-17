using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel
{
    public class ServerVoiceInviteConfiguration : IEntityTypeConfiguration<ServerVoiceInvite>
    {
        //maybe extend to allow "guest link" for voicechannel which basically works as the user is allowed into only that specific channel but once they leave they are kicked from the server
        public ulong ChannelId { get; set; } //must have a voicechannel
        public bool GuestInvite { get; set; } //auto-kick when leaving voicecall (wont be implemented at first but maybe later)
        public ServerVoiceChannelConfiguration Channel { get; set; } //if the invite is to a specific voicechannel

        public void Configure(EntityTypeBuilder<ServerVoiceInvite> builder)
        {
            builder.HasKey(b => new { b.SubjectId, b.InviterId });

            builder.Property(b => b.InviteCode).IsRequired();
            builder.Property(b => b.ExpirationTime).IsRequired(false);
            builder.Property(b => b.TimesUsed).IsRequired();
            builder.Property(b=>b.TotalUses).IsRequired(false);
            builder.Property(b => b.GuestInvite).IsRequired();

            builder.HasOne(b=>b.Subject).WithMany().HasForeignKey(b=>b.SubjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Inviter).WithMany().HasForeignKey(b => b.InviterId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Channel).WithMany(b => b.VoiceInvites).HasForeignKey(b => b.ChannelId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
/*
        public string InviteCode { get; set; } //essentially unique id can never be reused
        public DateTime? ExpirationTime { get; set; } // null = permanent
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; } //0 means unlimited //mayb review / set to nullable?
 */