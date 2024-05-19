using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.Settings
{
    public class WindowSettingsConfiguration : IEntityTypeConfiguration<WindowSettings>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool OpenEchoOnPCStartup { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeOnClose { get; set; }

        public AccountSettings AccountSettings { get; set; }

        public void Configure(EntityTypeBuilder<WindowSettings> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.OpenEchoOnPCStartup);
            builder.Property(b => b.StartMinimized);
            builder.Property(b => b.MinimizeOnClose);

            builder.HasOne(b => b.AccountSettings).WithOne().HasForeignKey<WindowSettings>(b=>b.Id);
        }
    }
}
