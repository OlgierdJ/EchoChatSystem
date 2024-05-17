using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using DomainCoreApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseEntityController<ProfileReport, ulong>
    {
        public ReportController(IEntityService<ProfileReport, ulong> service, IPushNotificationService notificationService) : base(service, notificationService)
        {
        }
    }
}
