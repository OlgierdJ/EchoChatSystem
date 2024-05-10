﻿using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerChannelCategory : BaseEntity<ulong>
    {
        public ulong ServerId { get; set; }
        public Server Server {  get; set; }
        public string Name { get; set; } //Names can be duplicate but categories will be sorted based on ids
        public DateTime DateCreated { get; set; } //idk nice to have maybe redundant if we make an audit log
        public bool IsPrivate { get; set; } //privatises the category and all channels within it
        public List<ServerTextChannel> TextChannels { get; set; }
        public List<ServerVoiceChannel> VoiceChannels { get; set; }
        public List<ServerChannelCategoryMemberSettings> ChannelCategoryMemberSettings { get; set; } //advanced permission settings per role which are enforced upon all channels in the category
    }
}
