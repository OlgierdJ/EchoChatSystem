﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.Management
{
    public class AccountServerFolder : BaseEntity<ulong> //figure out id for this shitty entity.
    {
        //public ulong AccountId { get; set; }
        public int Importance { get; set; } //used for ordering folders
        public string Name { get; set; } //name of the folder
        public string? Colour { get; set; } //null = default, else set in hex string

        public Account Account { get; set; } //owner
        public ICollection<ServerProfile>? Servers { get; set; } //servers within the folder //maybe map servers directly through seperate relation table instead of through serverprofile? idk
    }
}
