using CoreLib.DTO.EchoCore.UserCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace EchoWebapp.Components
{
    public class CustomRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {

        public CustomRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {

        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
