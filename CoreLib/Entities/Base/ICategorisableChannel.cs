namespace CoreLib.Entities.Base
{
    public interface ICategorisableChannel<TChannelCategory, TChannelCategoryId> : IChannel, ICategorisable<TChannelCategory, TChannelCategoryId>
    {//integrations? webhooks? invites? channelfollows?
        public TChannelCategoryId? CategoryId { get; set; }
        public TChannelCategory? Category { get; set; }
    }
}