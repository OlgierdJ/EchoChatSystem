using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class FriendSuggestion : BaseEntity<ulong> 
    {
        /*
         * accepting the suggestion consumes the friend suggestion
         * but declining it sets a flag allowing it to stay to prevent further suggestions to the same person
         */
        public string AccountId { get; set; }
        public string SuggestedFriendId { get; set; }
        public Account Account { get; set; }
        public Account SuggestedFriend { get; set; }
        public DateTime TimeCreated { get; set; }
        public bool Declined {  get; set; }
    }
}
