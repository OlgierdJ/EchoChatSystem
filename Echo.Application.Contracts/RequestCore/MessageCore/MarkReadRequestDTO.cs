﻿namespace Echo.Application.Contracts.RequestCore.MessageCore;

public class MarkReadRequestDTO
{
    //public ulong Id { get; set; } //user from jwt
    public ulong SubjectId { get; set; }
}