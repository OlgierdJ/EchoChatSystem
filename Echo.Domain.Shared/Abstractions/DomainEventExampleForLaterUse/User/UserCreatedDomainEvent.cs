using Echo.Domain.Shared.Abstractions;

namespace Echo.Domain.Shared.Abstractions.DomainEventExampleForLaterUse.User;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
