using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class FriendRequestSettingsConfiguration : IEntityTypeConfiguration<FriendRequestSettings>
{
    public void Configure(EntityTypeBuilder<FriendRequestSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff
        builder.Property(b => b.Everyone).HasDefaultValue(true).IsRequired();
        builder.Property(b => b.FriendsOfFriends).HasDefaultValue(false).IsRequired();
        builder.Property(b => b.ServerMembers).HasDefaultValue(false).IsRequired();
        //builder.HasOne(b => b.RequestedAccount).WithOne().HasForeignKey<FriendRequestSettings>(b => b.AccountId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        builder.HasOne(b => b.AccountSettings).WithOne(e => e.FriendRequestSettings).HasForeignKey<FriendRequestSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
