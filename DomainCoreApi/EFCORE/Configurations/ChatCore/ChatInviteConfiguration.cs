﻿using CoreLib.Entities.Base;
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
    public class ChatInviteConfiguration : IEntityTypeConfiguration<ChatInvite>
    {
        public void Configure(EntityTypeBuilder<ChatInvite> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.InviteCode)
                .IsRequired();
            builder
                .Property(b => b.ExpirationTime)
                .IsRequired(false);
            builder
               .Property(b => b.TimesUsed)
               .IsRequired();
            builder
               .Property(b => b.TotalUses)
               .IsRequired();
            builder.HasOne(b => b.Inviter).WithMany().HasForeignKey(b => b.InviterId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //maybe check if invites are automatically deleted when the one who creates it leaves a chat or server.
            builder.HasOne(b => b.Subject).WithMany(b => b.Invites).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}