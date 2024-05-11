using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class ChatService : BaseEntityService<Chat, ulong>, IChatService
    {
        public ChatService(IRepository<Chat> repository) : base(repository)
        {
        }
    }
}