using CoreLib.Entities.EchoCore.ChatCore;
using DomainCoreApi.EFCORE.ValueGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatInviteConfiguration : IEntityTypeConfiguration<ChatInvite>
    {
        public void Configure(EntityTypeBuilder<ChatInvite> builder)
        {
            builder
              .HasKey(b => b.InviteCode);
            builder.Property(e => e.InviteCode).HasMaxLength(10).HasValueGeneratorFactory<RandomStringIdGeneratorFactory>();
            builder
                .Property(b => b.ExpirationTime)
                .IsRequired(false);
            builder
               .Property(b => b.TimesUsed)
               .IsRequired();
            builder
               .Property(b => b.TotalUses)
               .IsRequired();
            builder.HasOne(b => b.Inviter).WithMany(e => e.ChatInvites).HasForeignKey(b => b.InviterId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(); //maybe check if invites are automatically deleted when the one who creates it leaves a chat or server.
            builder.HasOne(b => b.Subject).WithMany(b => b.Invites).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
