namespace CoreLib.Entities.Base
{
    public interface ITargetedNote<TAuthor, TAuthorId, TSubject, TSubjectId> : INote
    {
        public TAuthorId AuthorId { get; set; }
        public TSubjectId SubjectId { get; set; }

        public TAuthor Author { get; set; }
        public TSubject Subject { get; set; }
    }

    public interface INote
    {
        public string Note { get; set; }
    }
}
