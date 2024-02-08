using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatParticipancyConfiguration : IEntityTypeConfiguration<ChatParticipancy>
    {
        public void Configure(EntityTypeBuilder<ChatParticipancy> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
               .Property(b => b.TimeJoined).ValueGeneratedOnAdd()
               .IsRequired();
            builder.HasOne(b => b.Subject).WithMany().HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Participant).WithMany().HasForeignKey(b => b.ParticipantId).OnDelete(DeleteBehavior.Cascade).IsRequired(); // have changed ClientCascade to cascade
        }
    }
}
