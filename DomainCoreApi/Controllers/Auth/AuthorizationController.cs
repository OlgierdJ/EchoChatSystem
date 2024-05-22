using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Services;
using CoreLib.Interfaces;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController
    {
        public AuthorizationController(IUserActionService service, IPushNotificationService notificationService)
        {
            //_Service = service;
        }
    }
}
