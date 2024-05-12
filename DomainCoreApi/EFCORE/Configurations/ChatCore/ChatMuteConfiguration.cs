using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ChatCore;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatMuteConfiguration : IEntityTypeConfiguration<ChatMute>
    {
        public void Configure(EntityTypeBuilder<ChatMute> builder)
        {
            builder.HasKey(b => new { b.MuterId, b.SubjectId });
            builder.Property(b => b.TimeMuted).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(b => b.ExpirationTime).IsRequired(false);
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Muter).WithMany(b => b.MutedChats).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
