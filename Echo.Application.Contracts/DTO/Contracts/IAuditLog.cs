namespace Echo.Application.Contracts.DTO.Contracts;

public interface IAuditLog
{
    string Action { get; set; }
    DateTime TimeLogged { get; set; }

}
public interface IAuditLog<TUser> : IAuditLog
{
    TUser User { get; set; }
}