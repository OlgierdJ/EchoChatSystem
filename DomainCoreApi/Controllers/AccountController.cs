using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseEntityController<Account, ulong>
    {
        public AccountController(IEntityService<Account, ulong> service, IPushNotificationService notificationService) : base(service, notificationService)
        {
        }

    }
}
