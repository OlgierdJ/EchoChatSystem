namespace CoreLib.Entities.Base
{
    public abstract class BaseNickname<TAuthor, TAuthorId, TSubject, TSubjectId>
    {
        public TAuthorId AuthorId { get; set; }
        public TSubjectId SubjectId { get; set; }

        public string Nickname { get; set; }

        public TAuthor Author { get; set; }
        public TSubject Subject { get; set; }
    }
}
