using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore;

public class ServerRegionConfiguration : IEntityTypeConfiguration<ServerRegion> //needs different regions depending on the scope of the desired region aswell as corresponding regionserverurl.
                                                                                //(this is the url to the rtc server) f.eks
                                                                                //("Mr Worldwide", "Brazil-Flag", "https://echo.chat/rtc/brazil/rtchub")

{
    public void Configure(EntityTypeBuilder<ServerRegion> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Icon).IsRequired();
        builder.Property(b => b.RegionServerURL).IsRequired();

        builder.HasMany(b => b.VoiceChannels).WithOne(b => b.Region).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.ServerSettings).WithOne(b => b.Region).HasForeignKey(b => b.RegionId).OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new ServerRegion
        {
            Id = 1,
            Name = "Mr Worldwide",
            Icon = "https://upload.wikimedia.org/wikipedia/commons/4/4a/Brazilian_flag_icon_round.svg",
            RegionServerURL = "https://echo.chat/rtc/brazil/rtchub"
        });
        builder.HasData(new ServerRegion
        {
            Id = 2,
            Name = "holy pop",
            Icon = "https://en.wikipedia.org/wiki/St._Peter%27s_Basilica#/media/File:Basilica_di_San_Pietro_in_Vaticano_September_2015-1a.jpg",
            RegionServerURL = "https://echo.chat/rtc/vatikanet/rtchub"
        });
    }
}