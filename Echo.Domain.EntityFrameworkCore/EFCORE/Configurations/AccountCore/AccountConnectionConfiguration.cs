using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;

public class AccountConnectionConfiguration : IEntityTypeConfiguration<AccountConnection>
{
    public void Configure(EntityTypeBuilder<AccountConnection> builder)
    {
        builder.HasKey(b => b.Id);
        //not implemented
        //builder.Property(b=>b.AuthorizeResponseType).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeClientId).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeState).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        builder.HasOne(b => b.ConnectionType).WithMany(b => b.AccountConnections).HasForeignKey(b => b.ConnectionId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        builder.HasOne(b => b.Account).WithMany(b => b.Connections).HasForeignKey(b => b.AccountId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
