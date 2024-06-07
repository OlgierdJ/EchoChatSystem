namespace CoreLib.Interfaces.Contracts
{
    public interface ISuggestion<TReceiver, TReceiverId, TSuggestion, TSuggestionId> // : BaseEntity<TId>
    {
        public TReceiverId ReceiverId { get; set; }
        public TReceiver Receiver { get; set; }
        public TSuggestionId SuggestionId { get; set; }
        public TSuggestion Suggestion { get; set; }
        public DateTime TimeSuggested { get; set; }
    }
}
