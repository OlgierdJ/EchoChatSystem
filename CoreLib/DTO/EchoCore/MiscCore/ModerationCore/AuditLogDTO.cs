using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IAuditLog
    {
        string Action { get; set; }
        DateTime TimeLogged { get; set; }
        
    }

    public interface IAuditLog<TUser> : IAuditLog
    {
        TUser User { get; set; }
    }


    public class AuditLogDTO : IAuditLog, IAuditLog<UserMinimalDTO>
    {
        //public ulong Id { get; set; } id is redundant on clientside cause you can only view audit and they are sorted by time logged.
        public DateTime TimeLogged { get; set; }
        public string Action { get; set; }
        public UserMinimalDTO User { get; set; } //displayedUser

    }
}