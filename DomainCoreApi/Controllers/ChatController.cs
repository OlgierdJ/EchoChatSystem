using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseEntityController<Chat, ulong>
    {
        public ChatController(IEntityService<Chat, ulong> service, IPushNotificationService notificationService) : base(service, notificationService)
        {
        }
    }
}
