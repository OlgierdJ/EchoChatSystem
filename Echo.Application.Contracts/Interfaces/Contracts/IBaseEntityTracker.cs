namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IBaseEntityTracker<TOwner, TOwnerId, TCoOwner, TCoOwnerId, TSubject, TSubjectId> : IOwned<TOwner, TOwnerId>  //: BaseEntity<TId>
{
    public TCoOwnerId CoOwnerId { get; set; } //tracked entity
    public TSubjectId? SubjectId { get; set; } //entity that holds the tracker (cascade)
   
    public TCoOwner CoOwner { get; set; }
    public TSubject? Subject { get; set; }
}
