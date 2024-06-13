using Echo.Domain.Shared.Abstractions;

namespace Echo.Domain.Shared.Abstractions.DomainEventExampleForLaterUse.User;

public sealed record Name
{
    public Name(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
