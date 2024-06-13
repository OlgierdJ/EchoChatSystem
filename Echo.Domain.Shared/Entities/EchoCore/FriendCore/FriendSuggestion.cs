using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.FriendCore;

public class FriendSuggestion : ISuggestion<Account, ulong, Account, ulong>, IDomainEntity
{
    /*
     * accepting the suggestion consumes the friend suggestion
     * but declining it sets a flag allowing it to stay to prevent further suggestions to the same person
     */
    public bool Declined { get; set; } // can you inherently decline all suggestions? maybe move it to base?
    public ulong ReceiverId { get; set; }
    public Account Receiver { get; set; }
    public ulong SuggestionId { get; set; }
    public Account Suggestion { get; set; }
    public DateTime TimeSuggested { get; set; }
}
