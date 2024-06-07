namespace CoreLib.Entities.Base
{
    public interface IChannel
    {
        public string Name { get; set; }
        public string? Topic { get; set; } //channel topic description
        public int SlowMode { get; set; } //default 0 //dunno mayb change this to timespan or enum for second, hour options
        public bool IsAgeRestricted { get; set; }
        public bool IsPrivate { get; set; }
    }
    public interface IOwnedChannel<TChannelOwner, TChannelOwnerId> : IChannel, IOwned<TChannelOwner, TChannelOwnerId>
    {
        public TChannelOwnerId? OwnerId { get; set; }
        public TChannelOwner? Owner { get; set; }
    }
}