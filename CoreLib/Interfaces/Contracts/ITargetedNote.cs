namespace CoreLib.Interfaces.Contracts;

public interface ITargetedNote<TAuthor, TAuthorId, TSubject, TSubjectId> : INote
{
    public TAuthorId AuthorId { get; set; }
    public TSubjectId SubjectId { get; set; }

    public TAuthor Author { get; set; }
    public TSubject Subject { get; set; }
}
