using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelAccountMessageTrackerConfiguration : IEntityTypeConfiguration<ServerTextChannelAccountMessageTracker>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelAccountMessageTracker> builder)
        {
            builder.HasKey(b => new { b.OwnerId,b.CoOwnerId,b.SubjectId });

            builder.HasOne(b=>b.Owner).WithMany(b=>b.TextChannelMessageTrackers).HasForeignKey(b=>b.OwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasOne(b => b.CoOwner).WithMany(b=>b.MessageTrackers).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasOne(b => b.Subject).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
        }
    }
}
