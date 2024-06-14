﻿using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;


public class StatefulPermissionDTO : IStatefulPermission
{
    public ulong Id { get; set; }
    public string Name { get; set; } //Example app_view_admin_userinterface
    public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
}