namespace CoreLib.Entities.Base
{
    public interface IInvite<TInviter, TInviterId, TSubject, TSubjectId>
    {
        public string InviteCode { get; set; } //essentially unique id can never be reused
        public DateTime? ExpirationTime { get; set; } // null = permanent
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; } //0 means unlimited //mayb review / set to nullable?


        public TSubjectId SubjectId { get; set; }
        public TInviterId InviterId { get; set; }

        public TInviter Inviter { get; set; }
        public TSubject Subject { get; set; }
    }

}
