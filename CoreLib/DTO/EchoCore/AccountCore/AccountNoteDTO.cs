using CoreLib.Entities.Base;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountNoteDTO //: BaseNote<ulong, Account, ulong, Account, ulong>
    {
        public ulong SubjectId { get; set; }

        public string Note { get; set; }
    }
}