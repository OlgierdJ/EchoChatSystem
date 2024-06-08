namespace CoreLib.Interfaces.Contracts
{
    public interface IParticipable<TParticipant>
    {
        public ICollection<TParticipant>? Participants { get; set; }
    }

}
