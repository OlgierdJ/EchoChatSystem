namespace CoreLib.Entities.Base
{
    public abstract class BaseChannel : BaseEntity<ulong>
    {
        public string Name { get; set; }
        public string Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0
        public bool IsPrivate { get; set; }
    }
    public abstract class BaseChannel<TChannelCategory, TChannelCategoryId, TChannelPermission> : BaseChannel
    {//integrations? webhooks? invites? channelfollows?

        public TChannelCategoryId CategoryId { get; set; }
        public TChannelCategory? Category { get; set; }
        public ICollection<TChannelPermission> Permissions { get; set; }
    }
}