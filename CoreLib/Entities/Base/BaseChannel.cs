using CoreLib.Entities.EchoCore.ServerCore;

namespace CoreLib.Entities.Base
{
    public abstract class BaseChannel<TId> : BaseEntity<TId>
    {
        public string Name { get; set; }
        public string? Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0 //dunno mayb change this to timespan or enum for second, hour options
        public bool IsAgeRestricted { get; set; }
        public bool IsPrivate { get; set; }
    }
    //public abstract class BaseChannel<TId, TChannelPermission> : BaseChannel<TId>
    //public abstract class BaseChannel<TId, TChannelPermission> : BaseChannel<TId>
    //{
    //    public ICollection<TChannelPermission>? Permissions { get; set; }
    //}
    //public abstract class BaseChannel<TId, TChannelPermission, TChannelOwner, TChannelOwnerId> : BaseChannel<TId, TChannelPermission>
    public abstract class BaseChannel<TId, TChannelOwner, TChannelOwnerId> : BaseChannel<TId>
    {
        public TChannelOwnerId? OwnerId { get; set; }
        public TChannelOwner? Owner { get; set; }
    }
    //public abstract class BaseChannel<TId, TChannelPermission, TChannelCategory, TChannelCategoryId, TChannelOwner, TChannelOwnerId> : BaseChannel<TId, TChannelPermission, TChannelOwner, TChannelOwnerId>
    public abstract class BaseChannel<TId, TChannelCategory, TChannelCategoryId, TChannelOwner, TChannelOwnerId> : BaseChannel<TId, TChannelOwner, TChannelOwnerId>
    {//integrations? webhooks? invites? channelfollows?
        public TChannelCategoryId? CategoryId { get; set; }
        public TChannelCategory? Category { get; set; }
    }
}