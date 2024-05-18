using CoreLib.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController 
    {
        public AuthenticationController(IAuthenticationService service) 
        {
        }
    }
}
