namespace CoreLib.Interfaces.Contracts
{
    public interface IAuthoredEntity<TAuthor, TAuthorId>
    {
        public TAuthorId? AuthorId { get; set; } //mayb not nullable since you cant delete account? or was it because system messages has no author? i cant remember cause 2 hr sleep lol
        public TAuthor? Author { get; set; }
    }
}
