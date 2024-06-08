using CoreLib.Abstractions;

namespace Domain.Users;

public sealed class Usera : Entity
{
    private Usera(Guid id, Email email, Name name, bool hasPublicProfile)
        : base(id)
    {
        Email = email;
        Name = name;
        HasPublicProfile = hasPublicProfile;
    }

    private Usera()
    {
    }

    public Email Email { get; private set; }

    public Name Name { get; private set; }
    public bool HasPublicProfile { get; set; }

    public static Usera Create(Email email, Name name, bool hasPublicProfile)
    {
        var user = new Usera(Guid.NewGuid(), email, name, hasPublicProfile);

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
