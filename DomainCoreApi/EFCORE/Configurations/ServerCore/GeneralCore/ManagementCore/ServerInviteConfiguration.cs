using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ManagementCore
{
    public class ServerInviteConfiguration : IEntityTypeConfiguration<ServerInvite>
    {
        public void Configure(EntityTypeBuilder<ServerInvite> builder)
        {
            builder.HasKey(b => new { b.SubjectId, b.InviterId });


            builder.Property(b => b.InviteCode).IsRequired();
            builder.Property(b => b.ExpirationTime).IsRequired(false);
            builder.Property(b => b.TimesUsed).IsRequired();
            builder.Property(b => b.TotalUses).IsRequired();

            builder.HasOne(b => b.Subject).WithMany(e=>e.Invites).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Inviter).WithMany(e=>e.ServerInvites).HasForeignKey(b => b.InviterId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.TextChannel).WithMany(b => b.Invites).HasForeignKey(b => b.TextChannelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.VoiceChannel).WithMany(b => b.Invites).HasForeignKey(b => b.VoiceChannelId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
