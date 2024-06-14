using Echo.Domain.Shared.Entities.EchoCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore;


public class ConnectionTypeConfiguration : IEntityTypeConfiguration<ConnectionType>
{
    public void Configure(EntityTypeBuilder<ConnectionType> builder)
    {
        builder.HasKey(b => b.Id);
        //not implemented
        //builder.Property(b=>b.AuthorizeResponseType).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeClientId).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeState).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        //builder.Property(b=>b.AuthorizeCodeChallenge).HasMaxLength(256).IsRequired();
        builder.HasMany(b => b.AccountConnections).WithOne(b => b.ConnectionType).HasForeignKey(b => b.ConnectionId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        builder.HasData(new ConnectionType()
        {
            Id = 1,
            PlatformIcon = "PayPalIcon.png",
            PlatformName = "PayPal",
            AuthenticationEndpoint = "PayPal.com/api/v3/authenticate", //login with credentials
            AuthorizeEndpoint = "PayPal.com/api/v3/authorize",
            AuthorizationEndpoint = "PayPal.com/api/v3/authorization",
            TokenRefreshEndpoint = "PayPal.com/api/v3/token/refresh", //refresh login via token instead of credentials
            ExchangeReturnEndpoint = "PayPal.com/api/v3/token/exchange", //exchange token for different token
            TokenCheckEndpoint = "PayPal.com/api/v3/token/validate", //validate token against issued tokens to make sure the token has not been logged out / dropped
        });
        builder.HasData(new ConnectionType()
        {
            Id = 2,
            PlatformIcon = "CreditCard.png",
            PlatformName = "Card",
            AuthenticationEndpoint = "stripe.com/api/v3/authenticate", //login with credentials
            AuthorizeEndpoint = "stripe.com/api/v3/authorize",
            AuthorizationEndpoint = "stripe.com/api/v3/authorization",
            TokenRefreshEndpoint = "stripe.com/api/v3/token/refresh", //refresh login via token instead of credentials
            ExchangeReturnEndpoint = "stripe.com/api/v3/token/exchange", //exchange token for different token
            TokenCheckEndpoint = "stripe.com/api/v3/token/validate", //validate token against issued tokens to make sure the token has not been logged out / dropped
        });
    }
}
