using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseEntityController<Account, ulong>
    {
        //private readonly IAccountService _Service;
        public AccountController(IAccountService service, IPushNotificationService notificationService) : base(service, notificationService)
        {
            //_Service = service;
        }
    }
}
