﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.SettingsCore
{
    public class ServerWebhookConfiguration : BaseEntity<ulong>
    {
        //where to find inspiration of implementation
        //https://discord.com/developers/docs/resources/webhook
        public ulong ServerId { get; set; }
        public ulong TextChannelId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string WebhookEndpointURL { get; set; }
        //https://discord.com/api/webhooks/1214486375198363708/bO7ioVXfzPFnxfI2Gitcy9wLwLU7oD_5p5BjljQeVX_aTALnhtHQItHAtXCQtNqH4wDg
        //https://discord.com/api/webhooks/1214487431173382194/thdqRsUDC9Z_dgIGG76JvLaCgY6ne0RAQkig6vxBnwViBdjZgN5s4CSiHDtdxWiStBfQ
        public ServerConfiguration Server { get; set; }
        public ServerTextChannel TextChannel { get; set; }
    }
}