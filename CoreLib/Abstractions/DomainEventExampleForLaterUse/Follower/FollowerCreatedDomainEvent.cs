using CoreLib.Abstractions;

namespace Domain.Followers;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowedId) : IDomainEvent;
