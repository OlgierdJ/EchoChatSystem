using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerWebhookConfiguration : IEntityTypeConfiguration<ServerWebhook>
    {
        public void Configure(EntityTypeBuilder<ServerWebhook> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.ImageUrl).IsRequired();
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.WebhookEndpointURL).IsRequired();

            builder.HasOne(b => b.Server).WithMany().HasForeignKey(b=>b.ServerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.TextChannel).WithMany(b => b.Webhooks).HasForeignKey(b=>b.TextChannelId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
