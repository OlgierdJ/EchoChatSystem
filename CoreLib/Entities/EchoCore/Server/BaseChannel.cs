using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.Server
{
    public class BaseChannel : BaseEntity<int>
    {//integrations? webhooks? invites? channelfollows?
        public string Name { get; set; }
        public string Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0
        public bool IsPrivate { get; set; } 
        public int ChannelCategoryId { get; set; }
        public ChannelCategory? Category { get; set; }
        public List<ChannelPermission> Permissions { get; set; }
    }
}