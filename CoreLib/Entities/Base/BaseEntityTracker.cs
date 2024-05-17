namespace CoreLib.Entities.Base
{
    public abstract class BaseEntityTracker<TOwner, TOwnerId, TCoOwner, TCoOwnerId, TSubject, TSubjectId>  //: BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; } //owner entity (cascade)
        public TCoOwnerId CoOwnerId { get; set; } //tracked entity
        public TSubjectId? SubjectId { get; set; } //entity that holds the tracker (cascade)
        public TOwner Owner { get; set; }
        public TCoOwner CoOwner { get; set; }
        public TSubject? Subject { get; set; }
    }
}
