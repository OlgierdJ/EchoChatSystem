namespace Echo.Domain.Shared.Abstractions.DomainEventExampleForLaterUse.Follower;

public interface IFollowerRepository
{
    Task<bool> IsAlreadyFollowingAsync(
        Guid userId,
        Guid followedId,
        CancellationToken cancellationToken);

    void Insert(Follower follower);
}
