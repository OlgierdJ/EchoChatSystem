﻿namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class NicknameUserRequestDTO //adds personal nickname on user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Nickname { get; set; }
    }
}
