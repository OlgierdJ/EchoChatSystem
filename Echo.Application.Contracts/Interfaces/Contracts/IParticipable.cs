namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IParticipable<TParticipant>
{
    public ICollection<TParticipant>? Participants { get; set; }
}
