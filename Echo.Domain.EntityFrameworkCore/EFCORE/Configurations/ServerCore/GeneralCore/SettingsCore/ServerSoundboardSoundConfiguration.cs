using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore;

public class ServerSoundboardSoundConfiguration : IEntityTypeConfiguration<ServerSoundboardSound>
{
    public void Configure(EntityTypeBuilder<ServerSoundboardSound> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.SoundFileUrl).IsRequired();

        builder.HasOne(b => b.AssociatedEmote).WithMany(b => b.SoundboardSounds).HasForeignKey(b => b.AssociatedEmoteId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        builder.HasOne(b => b.Server).WithMany(b => b.SoundboardSounds).HasForeignKey(b => b.ServerId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Uploader).WithMany().HasForeignKey(b => b.UploaderId).OnDelete(DeleteBehavior.Restrict);
    }
}
