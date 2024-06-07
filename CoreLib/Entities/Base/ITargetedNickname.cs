namespace CoreLib.Entities.Base
{
    public interface ITargetedNickname<TAuthor, TAuthorId, TSubject, TSubjectId> : INickname
    {
        public TAuthorId AuthorId { get; set; }
        public TSubjectId SubjectId { get; set; }

        public string Nickname { get; set; }

        public TAuthor Author { get; set; }
        public TSubject Subject { get; set; }
    }

    public interface INickname
    {
        public string Nickname { get; set; }

    }
}
