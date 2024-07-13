using Echo.Domain.Shared.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OpenIddict.Validation.AspNetCore;

namespace DomainCoreApi.Hubs;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class DomainPushNotificationHub : Hub<IDomainNotificationHub>
{
    //simple hub only one call that offloads logic to listeners
    //only servers should consume this api we dont have time to
    //implement specific accesscontrol for this hub.
    //usually this would be done via some form of access-token
    //issued by an authentication server towards this server domain.
}
