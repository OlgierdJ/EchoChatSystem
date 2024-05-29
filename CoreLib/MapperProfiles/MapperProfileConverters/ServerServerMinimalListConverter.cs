using AutoMapper;
using CoreLib.DTO.EchoCore.ServerCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.MapperProfiles.MapperProfileConverters
{
    public interface IServerListServerMinimalListConverter : ITypeConverter<IList<Server>, IList<ServerMinimalDTO>>
    {
    }
    public class ServerListServerMinimalListConverterFactory
    {
        private readonly IServerListServerMinimalListConverter resolver;
        public ServerListServerMinimalListConverterFactory()
        {
            resolver = new ServerListServerMinimalListConverter(new Dictionary<ulong, ServerMinimalDTO>());
        }

        public object Resolve(Type t)
        {
            return t == typeof(IServerListServerMinimalListConverter) ? resolver : null;
        }
    }

    public class ServerListServerMinimalListConverter : IServerListServerMinimalListConverter
    {
        private readonly IDictionary<ulong, ServerMinimalDTO> existingMappings;

        public ServerListServerMinimalListConverter(IDictionary<ulong, ServerMinimalDTO> existingMappings)
        {
            this.existingMappings = existingMappings;
        }

        public IList<ServerMinimalDTO> Convert(IList<Server> source, IList<ServerMinimalDTO> destination, ResolutionContext context)
        {
            //var friendshipParticipancies = context.InstanceCache as IList<FriendshipParticipancy>;
            //var friendshipParticipancies = context.SourceValue as IList<FriendshipParticipancy>;
            if (source == null)
            {
                return null;
            }

            var dtos = new List<ServerMinimalDTO>();
            foreach (var server in source)
            {
                ServerMinimalDTO mapped = null;
                if (existingMappings.ContainsKey(server.Id))
                {
                    mapped = existingMappings[server.Id];
                }
                else
                {
                    mapped = context.Mapper.Map<ServerMinimalDTO>(server);
                    existingMappings[server.Id] = mapped;
                }
                dtos.Add(mapped);
            }

            return dtos;
        }
    }
}
