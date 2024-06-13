using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.FriendCore;

public class Friendship : BaseEntity<ulong>
{
    public ICollection<FriendshipParticipancy> Participants { get; set; }
    public DateTime TimeCreated { get; set; }
}