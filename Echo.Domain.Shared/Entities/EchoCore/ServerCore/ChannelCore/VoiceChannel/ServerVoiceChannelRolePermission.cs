﻿using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelRolePermission : IDomainEntity
//Used for displaying state of serverpermission for a specific channel for a specific role
{
    //combine foreign key channel and role.
    //pk is combination of channel, role and permission
    public ulong ChannelId { get; set; }
    public ulong RoleId { get; set; }
    public ulong PermissionId { get; set; }
    public bool? State { get; set; } //true = enabled, null = default, false = disabled
    public ServerVoiceChannelRole ChannelRole { get; set; } //cascade
    public ServerVoiceChannel Channel { get; set; } //ignore
    public ServerRole Role { get; set; } //ignore
    public ServerPermission Permission { get; set; } //cascade
}