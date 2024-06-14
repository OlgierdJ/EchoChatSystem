using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.ManagementCore;

public class ServerEventConfiguration : IEntityTypeConfiguration<ServerEvent>
{
    public void Configure(EntityTypeBuilder<ServerEvent> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Topic).IsRequired();
        builder.Property(b => b.Description).IsRequired(false);
        builder.Property(b => b.ImageFileUrl).IsRequired(false);
        builder.Property(b => b.EventFrequency).HasConversion<int>().IsRequired();
        builder.Property(b => b.StartTime).HasDefaultValueSql("getdate()").IsRequired();
        builder.Property(b => b.EndTime).IsRequired();
        builder.Property(b => b.Location).IsRequired(false);

        builder.HasOne(b => b.Server).WithMany(b => b.Events).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Creator).WithMany(b => b.ServerEvents).HasForeignKey(b => b.CreatorId).OnDelete(DeleteBehavior.NoAction);//tænker NoAction da vi ikke sletter account
    }
}