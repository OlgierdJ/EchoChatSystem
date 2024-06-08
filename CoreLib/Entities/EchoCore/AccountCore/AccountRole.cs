using CoreLib.Abstractions;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountRole : IDomainEntity
    //thought is that an Account can be both guest, registereduser, applicationsupport, applicationmoderater, applicationadmin, applicationsuperadmin, etc
    {
        public ulong AccountId { get; set; }
        public ulong RoleId { get; set; }
        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
