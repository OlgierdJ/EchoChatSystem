using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : BaseEntityController<Server, ulong>
    {
        public ServerController(IEntityService<Server, ulong> service, IPushNotificationService notificationService) : base(service, notificationService)
        {
        }
    }
}
