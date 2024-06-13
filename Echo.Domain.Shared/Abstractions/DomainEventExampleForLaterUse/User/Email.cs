using Echo.Domain.Shared.Abstractions;

namespace Echo.Domain.Shared.Abstractions.DomainEventExampleForLaterUse.User;

public sealed record Email
{
    private Email(string value) => Value = value;

    public string Value { get; }

    public static Email Create(string? email)
    {
        Ensure.NotNullOrEmpty(email);

        if (email.Split('@').Length != 2)
        {
            throw new ApplicationException("Invalid email");
        }

        return new Email(email);
    }
}
