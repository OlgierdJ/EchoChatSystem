using CoreLib.Entities.EchoCore.ServerCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class ServerService : BaseEntityService<Server, ulong>, IServerService
    {
        public ServerService(IRepository<Server> repository) : base(repository)
        {
        }
    }
}
