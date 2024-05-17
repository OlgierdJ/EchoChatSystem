using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class FriendSuggestion : BaseSuggestion<Account, ulong, Account, ulong>
    {
        /*
         * accepting the suggestion consumes the friend suggestion
         * but declining it sets a flag allowing it to stay to prevent further suggestions to the same person
         */
        public bool Declined { get; set; } // can you inherently decline all suggestions? maybe move it to base?
    }
}
