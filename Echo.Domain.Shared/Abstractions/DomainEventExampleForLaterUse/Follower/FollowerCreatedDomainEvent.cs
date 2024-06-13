using Echo.Domain.Shared.Abstractions;

namespace Echo.Domain.Shared.Abstractions.DomainEventExampleForLaterUse.Follower;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowedId) : IDomainEvent;
