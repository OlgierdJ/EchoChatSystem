using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ManagementCore;

public class ServerEventParticipancyConfiguration : IEntityTypeConfiguration<ServerEventParticipancy>
{
    public void Configure(EntityTypeBuilder<ServerEventParticipancy> builder)
    {
        builder.HasKey(b => new { b.ServerId, b.EventId, b.AccountId });

        builder.Property(b => b.TimeJoined).HasDefaultValueSql("getdate()").IsRequired();

        builder.HasOne(b => b.Server).WithMany(b => b.EventParticipancies).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Event).WithMany(b => b.Participants).HasForeignKey(b => b.EventId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(b => b.Account).WithMany(b => b.EventParticipancies).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.ServerProfile).WithMany(b => b.EventParticipancies).HasForeignKey(b => new { b.AccountId, b.ServerId }).OnDelete(DeleteBehavior.NoAction);
    }
}

