namespace CoreLib.Entities.Base
{
    public class BaseChannel : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0
        public bool IsPrivate { get; set; }
    }
    public class BaseChannel<TChannelCategory, TChannelCategoryId, TChannelPermission> : BaseChannel
    {//integrations? webhooks? invites? channelfollows?

        public TChannelCategoryId CategoryId { get; set; }
        public TChannelCategory? Category { get; set; }
        public ICollection<TChannelPermission> Permissions { get; set; }
    }
}