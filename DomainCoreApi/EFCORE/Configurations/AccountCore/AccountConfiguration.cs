﻿using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(256).IsRequired();
            builder.Property(b=> b.TimeCreated).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(b => b.TimeLastLogon).IsRequired(false);
            builder.Property(b => b.UserId).HasMaxLength(256).IsRequired(false);
            //builder.Property(b => b.FocusModeEnabled).HasDefaultValue(true).IsRequired();
            builder.HasOne(b => b.ActivityStatus).WithMany(b=>b.Accounts).HasForeignKey(b=>b.ActivityStatusId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //skal lige kig på det kan ikke lige find ud hvorfor den ikke vil får en "Compiler Error CS0452"
            builder.HasOne(b => b.CustomStatus).WithOne(b=>b.Account).HasForeignKey<Account>(b=>b.CustomStatusId).OnDelete(DeleteBehavior.Cascade).IsRequired(false); //skal lige kig på det kan ikke lige find ud hvorfor den ikke vil får en "Compiler Error CS0452"
            builder.HasOne(b => b.Profile).WithOne(b=>b.Account).HasForeignKey<AccountProfile>(b=>b.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //skal lige kig på det kan ikke lige find ud hvorfor den ikke vil får en "Compiler Error CS0452"
            builder.HasMany(e => e.Connections).WithOne(e => e.Account).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            builder.HasOne(b => b.Settings).WithOne(b=>b.Account).HasForeignKey<AccountSettings>(b=>b.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired(); //skal lige kig på det kan ikke lige find ud hvorfor den ikke vil får en "Compiler Error CS0452"
            builder.HasMany(e => e.Roles).WithMany(e => e.Recipients).UsingEntity<AccountRole>(
                l => l.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId),
                 r => r.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId)
            );
            builder.HasMany(e => e.Violations).WithOne(e => e.Subject).HasForeignKey(e=>e.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.IssuedViolations).WithOne(e => e.Issuer).HasForeignKey(e=>e.IssuerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ReviewedAppeals).WithOne(e => e.Reviewer).HasForeignKey(e=>e.ReviewerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.Sessions).WithOne(e => e.Account).HasForeignKey(e=>e.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.BlockedAccounts).WithOne(e => e.Blocker).HasForeignKey(e=>e.BlockerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.NicknamedAccounts).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.NotedAccounts).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.MutedVoices).WithOne(e => e.Muter).HasForeignKey(e=>e.MuterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.MutedChats).WithOne(e => e.Muter).HasForeignKey(e=>e.MuterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.MutedSoundboards).WithOne(e => e.Muter).HasForeignKey(e=>e.MuterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ChatMessageTrackers).WithOne(e => e.Owner).HasForeignKey(e=>e.OwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            //builder.HasMany(e => e.TextChannelMessageTrackers).WithOne(e => e.Owner).HasForeignKey(e=>e.OwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ReportedCustomStatuses).WithOne(e => e.Account).HasForeignKey(e=>e.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ReportedProfiles).WithOne(e => e.Account).HasForeignKey(e=>e.AccountId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ReportedMessages).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.CustomStatusReports).WithOne(e => e.Reporter).HasForeignKey(e=>e.ReporterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ProfileReports).WithOne(e => e.Reporter).HasForeignKey(e=>e.ReporterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.MessageReports).WithOne(e => e.Reporter).HasForeignKey(e=>e.ReporterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.IncomingFriendRequests).WithOne(e => e.Receiver).HasForeignKey(e=>e.ReceiverId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.OutgoingFriendRequests).WithOne(e => e.Sender).HasForeignKey(e=>e.SenderId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.Friendships).WithMany(e => e.Participants).UsingEntity<FriendshipParticipancy>(
                 l => l.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId),
                  r => r.HasOne(e => e.Participant).WithMany().HasForeignKey(e => e.ParticipantId)
             );
            builder.HasMany(e => e.FriendSuggestions).WithOne(e => e.Receiver).HasForeignKey(e=>e.ReceiverId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.Chats).WithMany(e => e.Participants).UsingEntity<ChatParticipancy>(
                l => l.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId),
                  r => r.HasOne(e => e.Participant).WithMany().HasForeignKey(e => e.ParticipantId)
                );
            builder.HasMany(e => e.ChatInvites).WithOne(e => e.Inviter).HasForeignKey(e=>e.InviterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(e => e.ChatMessages).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            //builder.HasMany(e => e.Servers).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            //builder.HasMany(e => e.ServerInvites).WithOne(e => e.Inviter).HasForeignKey(e=>e.InviterId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            //builder.HasMany(e => e.ChannelMessages).WithOne(e => e.Author).HasForeignKey(e=>e.AuthorId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        
        }
    }
}
