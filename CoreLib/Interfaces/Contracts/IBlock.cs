namespace CoreLib.Interfaces.Contracts
{
    public interface IBlock<TBlockerEntity, TBlockerEntityId, TBlockedEntity, TBlockedEntityId> //done
    {
        public TBlockerEntityId BlockerId { get; set; }
        public TBlockedEntityId BlockedId { get; set; }
        public DateTime TimeBlocked { get; set; }
        public TBlockerEntity Blocker { get; set; }
        public TBlockedEntity Blocked { get; set; }
    }
}
