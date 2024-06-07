using AutoMapper;
using CoreLib.Abstractions;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using CoreLib.Hubs;
using CoreLib.Interfaces;
using DomainPushNotificationApi.Hubs;
using DomainRTCApi.Hubs;
using DomainRTCApi.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;

namespace DomainPushNotificationApi.Services
{
    public class PushNotificationService
    {
        protected readonly IHubContext<PushNotificationHub, IPushNotificationHub> _hubContext;
        private Dictionary<(string,EntityAction), Func<object,Task>> _hubManager = new();
        public PushNotificationService(IHubContext<PushNotificationHub, IPushNotificationHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _hubManager.Add((nameof(AccountProfile),EntityAction.Added), async (entity) => 
            {
                AccountProfile account = entity as AccountProfile;
                await _hubContext.Clients.All.ReceiveUserDTOCreateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOCreateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOCreateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOCreateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOCreateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOCreateMessage(mapper.Map<MemberDTO>(account));
            });
            _hubManager.Add((nameof(AccountProfile), EntityAction.Modified), async (entity) =>
            {
                AccountProfile account = entity as AccountProfile;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(account));
            });
            _hubManager.Add((nameof(AccountProfile), EntityAction.Deleted), async (entity) =>
            {
                AccountProfile account = entity as AccountProfile;
                mapper.Map<UserFullDTO>(entity);
                mapper.Map<UserMinimalDTO>(entity);
                mapper.Map<UserDTO>(entity);
                mapper.Map<UserProfileDTO>(entity);
                mapper.Map<MemberDTO>(entity);
                await _hubContext.Clients.All.ReceiveUserDTODeleteMessage(mapper.Map<UserDTO>(account));
            });


        }

        public async Task NotifyClients(DomainEvent domain)
        {
            await Task.Run(async () =>
            {
                var task = _hubManager[(domain.Type,domain.Action)];
                await task(domain.Entity);
            });
        }

        //public async Task NotifyClients<T>(T entity, EntityAction action) where T : IEntity
        //{
        //    await Task.Run(async () =>
        //    {
        //        var task = _hubManager[typeof(T).Name];
        //        await task(entity, action);
        //    });
        //}

        //public Task NotifyClients<T>(IEnumerable<T> entities, EntityAction action) where T : IEntity
        //{
        //    throw new NotImplementedException();
        //}
    }
}
