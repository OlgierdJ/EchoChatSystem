using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountNote : ITargetedNote<Account, ulong, Account, ulong>
    {
        public ulong AuthorId { get; set; }
        public ulong SubjectId { get; set; }
        public string Note { get; set; }
        public Account Author { get; set; }
        public Account Subject { get; set; }
    }
}