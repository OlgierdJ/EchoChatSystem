namespace Echo.Domain.Shared.Interfaces.Services;

public interface IUserGroupService
{
    Task<IEnumerable<string>> GetGroups(ulong accountId);
}
