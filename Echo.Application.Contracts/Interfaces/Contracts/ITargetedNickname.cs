namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface ITargetedNickname<TAuthor, TAuthorId, TSubject, TSubjectId> : INickname
{
    public TAuthorId AuthorId { get; set; }
    public TSubjectId SubjectId { get; set; }

    public TAuthor Author { get; set; }
    public TSubject Subject { get; set; }
}
