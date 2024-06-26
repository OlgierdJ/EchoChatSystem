﻿using AutoMapper;
using CoreLib.DTO.EchoCore.ServerCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore.FriendCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.MapperProfiles.MapperProfileConverters
{
    public interface IFriendshipParticipancyListUserDTOListConverter : ITypeConverter<IList<FriendshipParticipancy>, IList<UserDTO>>
    {
    }
    public class EchoMappingConverterFactory
    {
        private readonly IFriendshipParticipancyListUserDTOListConverter friendshipParticipancyListUserDTOResolver;
        private readonly IServerListServerMinimalListConverter serverListServerMinimalListResolver;
        public EchoMappingConverterFactory()
        {
            friendshipParticipancyListUserDTOResolver = new FriendshipParticipancyListUserDTOListConverter(new Dictionary<ulong, UserDTO>());
            serverListServerMinimalListResolver = new ServerListServerMinimalListConverter(new Dictionary<ulong, ServerMinimalDTO>());
        }

        public object Resolve(Type t)
        {
            if (t == typeof(IFriendshipParticipancyListUserDTOListConverter))
            {
                return friendshipParticipancyListUserDTOResolver;
            }
            if (t == typeof(IServerListServerMinimalListConverter))
            {
                return serverListServerMinimalListResolver;
            }
            return null;
        }
    }

    public class FriendshipParticipancyListUserDTOListConverter : IFriendshipParticipancyListUserDTOListConverter
    {
        private readonly IDictionary<ulong, UserDTO> existingMappings;

        public FriendshipParticipancyListUserDTOListConverter(IDictionary<ulong, UserDTO> existingMappings)
        {
            this.existingMappings = existingMappings;
        }

        public IList<UserDTO> Convert(IList<FriendshipParticipancy> source, IList<UserDTO> destination, ResolutionContext context)
        {
            //var friendshipParticipancies = context.InstanceCache as IList<FriendshipParticipancy>;
            //var friendshipParticipancies = context.SourceValue as IList<FriendshipParticipancy>;
            if (source == null)
            {
                return null;
            }

            var dtos = new List<UserDTO>();
            foreach (var friendshipParticipancy in source)
            {
                UserDTO mapped = null;
                if (existingMappings.ContainsKey(friendshipParticipancy.ParticipantId))
                {
                    mapped = existingMappings[friendshipParticipancy.ParticipantId];
                }
                else
                {
                    mapped = context.Mapper.Map<UserDTO>(friendshipParticipancy);
                    existingMappings[friendshipParticipancy.ParticipantId] = mapped;
                }
                dtos.Add(mapped);
            }

            return dtos;
        }
    }
}
