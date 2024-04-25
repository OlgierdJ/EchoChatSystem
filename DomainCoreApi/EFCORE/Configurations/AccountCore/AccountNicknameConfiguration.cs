﻿using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountNicknameConfiguration : IEntityTypeConfiguration<AccountNickname>
    {
        public void Configure(EntityTypeBuilder<AccountNickname> builder)
        {
            builder.HasKey(b=>b.Id);
            builder.Property(e=>e.Nickname).HasMaxLength(120).IsRequired();
            builder.HasOne(b => b.Author).WithMany(b => b.NicknamedAccounts).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}