﻿using CoreLib.Entities.EchoCore.ServerCore;

namespace CoreLib.Entities.Base
{
    public abstract class BaseChannel<TId> : BaseEntity<TId>
    {
        public string Name { get; set; }
        public string? Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0
        public bool IsPrivate { get; set; }
    }
    public abstract class BaseChannel<TId, TChannelPermission> : BaseChannel<TId>
    {
        public ICollection<TChannelPermission>? Permissions { get; set; }
    }
    public abstract class BaseChannel<TId, TChannelPermission, TChannelCategory, TChannelCategoryId, TChannelOwner, TChannelOwnerId> : BaseChannel<TId, TChannelPermission>
    {//integrations? webhooks? invites? channelfollows?
        public TChannelOwnerId? OwnerId { get; set; }
        public TChannelOwner? Owner { get; set; }
        public TChannelCategoryId? CategoryId { get; set; }
        public TChannelCategory? Category { get; set; }
    }
}