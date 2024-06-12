namespace CoreLib.Interfaces.Services;

public interface IUserGroupService
{
    Task<IEnumerable<string>> GetGroups(ulong accountId);
}
