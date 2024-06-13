namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IRolePermission<TRole> : IPermission
{
    public ICollection<TRole>? Roles { get; set; }
}
