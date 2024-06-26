﻿using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.UserCore
{

    public class MembershipBadgeDTO : IMembershipBadge //probably instead of generating it each time via mapping one should probably generate it once in database
    {
        public int OrderingWeight { get; set; } //used for sorting
        public string IconURL { get; set; } //relative icon
        public string IconName { get; set; } //echo, server name, subscription name, etc
        public DateTime TimeJoined { get; set; } //date the user became a member of the specific thing
    }
}