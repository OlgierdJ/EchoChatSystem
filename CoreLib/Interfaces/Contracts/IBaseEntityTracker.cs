namespace CoreLib.Interfaces.Contracts
{
    public interface IBaseEntityTracker<TOwner, TOwnerId, TCoOwner, TCoOwnerId, TSubject, TSubjectId> : IOwned<TOwner, TOwnerId>  //: BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; } //owner entity (cascade)
        public TCoOwnerId CoOwnerId { get; set; } //tracked entity
        public TSubjectId? SubjectId { get; set; } //entity that holds the tracker (cascade)
        public TOwner Owner { get; set; }
        public TCoOwner CoOwner { get; set; }
        public TSubject? Subject { get; set; }
    }
}
