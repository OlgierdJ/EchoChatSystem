﻿using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMemberPermission : IDomainEntity
//Used for displaying state of serverpermission for a specific channel for a specific member
{
    //combine foreign key profile and role.
    //pk is combination of channel, role and permission
    public ulong ChannelId { get; set; }
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }
    public ulong PermissionId { get; set; }
    public bool? State { get; set; } //true = enabled, null = default, false = disabled
    public ServerTextChannelMemberSettings MemberSettings { get; set; } //cascade
    public ServerTextChannel Channel { get; set; } //ignore
    public ServerProfile Profile { get; set; } //ignore
    public Server Server { get; set; } //ignore
    public ServerPermission Permission { get; set; } //cascade
}