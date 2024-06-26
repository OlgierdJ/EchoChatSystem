﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations
{
    public class ServerBotIntegrationRoleRestriction : BaseEntity<ulong>
    {
        public ulong ServerBotIntegrationId { get; set; }
        public ulong ServerRoleId { get; set; }
        /// <summary>
        /// whether or not a certain role is permitted to use the bot. 
        /// (if bot is not permitted by everyone but is permitted by certain role then only if the user has the role they are allowed to use the bot)
        /// THIS RESTRICTION SHOULD BE OVERRIDDEN BY MEMBER RESTRICTIONS
        /// </summary>
        public bool Permitted { get; set; }
        public ServerBotIntegration ServerBotIntegration { get; set; }
        public ServerRole ServerRole { get; set; }
    }
}