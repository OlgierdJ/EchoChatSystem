namespace CoreLib.Entities.Base
{
    public interface IParticipable<TParticipant>
    {
        public ICollection<TParticipant>? Participants { get; set; }
    }

}
