﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class FriendRequestSettingsConfiguration : IEntityTypeConfiguration<FriendRequestSettings>
    {
        public void Configure(EntityTypeBuilder<FriendRequestSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff
            builder.Property(b=>b.Everyone).HasDefaultValue(true).IsRequired();
            builder.Property(b => b.FriendsOfFriends).HasDefaultValue(false).IsRequired();
            builder.Property(b => b.ServerMembers).HasDefaultValue(false).IsRequired();
            builder.HasOne(b => b.RequestedAccount).WithOne().HasForeignKey<FriendRequestSettings>(b => b.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasOne(b => b.AccountSettings).WithOne(e => e.FriendRequestSettings).HasForeignKey<FriendRequestSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
