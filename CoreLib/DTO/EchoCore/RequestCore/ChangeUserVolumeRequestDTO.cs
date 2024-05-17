﻿namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class ChangeUserVolumeRequestDTO //used to change specific user volume via slider
    {
        //public ulong SenderId { get; set; } get from jwt
        public ulong UserId { get; set; }
        public byte Volume { get; set; } //max 200 min 0
    }
}
