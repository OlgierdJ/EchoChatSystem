using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMuteConfiguration : IEntityTypeConfiguration<ChatMute>
    {
        public void Configure(EntityTypeBuilder<ChatMute> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimeMuted).ValueGeneratedOnAdd()
                .IsRequired();
            builder
                .Property(b => b.ExpirationTime)
                .IsRequired(false);
            builder.HasOne(b => b.Subject).WithMany(e => e.Mutes).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Muter).WithMany(e => e.MutedChats).HasForeignKey(b => b.MuterId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
