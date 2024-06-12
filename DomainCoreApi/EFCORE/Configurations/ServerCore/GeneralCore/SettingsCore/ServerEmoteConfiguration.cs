using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore;

public class ServerEmoteConfiguration : IEntityTypeConfiguration<ServerEmote>
{
    public void Configure(EntityTypeBuilder<ServerEmote> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.ImageUrl).IsRequired();

        builder.HasOne(b => b.Server).WithMany(b => b.Emotes).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Uploader).WithMany().HasForeignKey(b => b.UploaderId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.SoundboardSounds).WithOne(b => b.AssociatedEmote).HasForeignKey(b => b.AssociatedEmoteId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
    }
}